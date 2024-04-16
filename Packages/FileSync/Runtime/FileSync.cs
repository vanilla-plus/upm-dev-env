#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Xml;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

//using Vanilla.DeltaValues;

namespace Vanilla.FileSync
{

    [Serializable]
    public struct FetchableDirectory
    {

        public string directory;
        public bool   includeSubdirectories;

    }

    // Another step is required to make the bucket truly public on S3.

    // Enter your public bucket, go to the Permissions tab and paste in the following:
    // Don't forget to replace the bucket name!

    /*
    {
        "Version": "2012-10-17",
        "Statement": [
            {
                "Sid": "PublicReadGetObject",
                "Effect": "Allow",
                "Principal": "*",
                "Action": [
                    "s3:GetObject",
                    "s3:ListBucket"
                ],
                "Resource": [
                    "arn:aws:s3:::your-bucket-name-here/*",
                    "arn:aws:s3:::your-bucket-name-here"
                ]
            }
        ]
    }
    */

    public static partial class FileSync
    {

        
        private static string Remote_Root     = null;
        private static string Remote_List_URL = null;

        public static string Local_Root = "fetch";

        public static int Local_Path_Segments_To_Skip = 0;

        private const string XML_Namespace_Uri    = "http://s3.amazonaws.com/doc/2006-03-01/";
        private const string XML_Namespace_Prefix = "s3";

        private const string XML_Namespace_XPath = "//s3:Contents";

        private const string XML_Node_Key          = "s3:Key";
        private const string XML_Node_Size         = "s3:Size";
        private const string XML_Node_LastModified = "s3:LastModified";

        public static int Download_Chunk_Buffer_MBs      = 32;
        public static int Download_Chunk_Buffer_ByteSize = Download_Chunk_Buffer_MBs * 1024 * 1024;

        public static long FileMapSizeDiff = 0;

        public static long  CurrentDownloadBytesDownloaded = 0;
        public static float CurrentDownloadPercentComplete = 0.0f;

//        public static Action<float> OnDownloadProgress;

//        private const string XML_Node_Etag         = "s3:ETag";
//        private const string XML_Node_StorageClass = "s3:StorageClass";

        public static readonly HttpClient HTTPClient = new HttpClient();

        [NonSerialized]
        public static RemoteS3Object[] RemoteFileMap = Array.Empty<RemoteS3Object>();
        
        public static void Initialize(string remoteRoot, string localRoot, int rootPathsToSkip = 0)
        {
            Remote_Root     = remoteRoot;
            Remote_List_URL = $"{Remote_Root}?list-type=2&prefix=";
            Local_Root      = localRoot;
            Local_Path_Segments_To_Skip = rootPathsToSkip;
        }
//
//
//        public static string GetTruncatedLocalPath()
//        {
//            var keySegments = key.Split(sep);
//
//            var truncatedKey = string.Join(separator: sep,
//                                           values: keySegments.Skip());
//
//            return Path.Combine(Application.persistentDataPath,
//                                Local_Root,
//                                truncatedKey);
//        }


        public static async UniTask<RemoteS3Object[]> GetFileMap(IEnumerable<FetchableDirectory> directories,
                                                                 IEnumerable<string> files)
        {
            var fileMap = new HashSet<RemoteS3Object>();

            foreach (var entry in directories)
            {
                var remoteObjects = await GetRemoteObjects(s3Directory: entry.directory,
                                                           includeSubdirectories: entry.includeSubdirectories);

                foreach (var o in remoteObjects)
                {
                    if (!fileMap.Contains(o)) fileMap.Add(o);
                }
            }

            foreach (var entry in files)
            {
                var o = await GetRemoteObject(entry);

                if (!fileMap.Contains(o)) fileMap.Add(o);
            }

//            #if debug
//            Debug.Log("RemoteFileMap results:");
//            foreach (var f in fileMap) Debug.Log(f.RemoteFilePath);
//            #endif

            return fileMap.ToArray();
        }


        public static bool FileMapSyncRequired()
        {
            var syncRequired = false;

            CurrentDownloadBytesDownloaded = 0;
            FileMapSizeDiff                = 0;

            foreach (var f in RemoteFileMap)
            {
                if (!f.DownloadRequired) continue;

                syncRequired = true;

                FileMapSizeDiff += f.RemoteFileSize;
            }
            
            #if debug
            Debug.Log($"FileMapSizeDiff: [{FileMapSizeDiff}]");
            Debug.Log($"SyncRequired: [{syncRequired}]");
            #endif

            return syncRequired;
        }


        public static async UniTask SynchronizeFileMap(RemoteS3Object[] fileMap, int numberOfSimultaneousDownloads)
        {
            
            // ToDo - This is great for downloading, but shouldn't we also
            // ToDo - handle the removal of local files that are no longer present on remote as well?
            
            numberOfSimultaneousDownloads = Math.Clamp(value: numberOfSimultaneousDownloads,
                                                       min: 1,
                                                       max: 8);

            var filesToDownload = fileMap.Where(s3Object => s3Object.DownloadRequired).ToArray();
            
            int fileMapTotal         = filesToDownload.Length;
            int fileMapSegmentStride = fileMapTotal / numberOfSimultaneousDownloads;                         // How many indices should each segment be responsible for?
            var willHaveRemainder    = numberOfSimultaneousDownloads * fileMapSegmentStride != fileMapTotal; // Can we evenly segment the downloads or will there be over-hang?
            var segmentTotal         = -1;
            
            var segments = new List<UniTask>();

//            await UniTask.SwitchToThreadPool();
            await UniTask.SwitchToTaskPool();

            for (var i = 0;
                 i < numberOfSimultaneousDownloads;
                 i++)
            {
                var startIndex = i * fileMapSegmentStride;
                var endIndex   = ((i+1) * fileMapSegmentStride) - 1;

                // This is hacky but it works fine.
                // If this is the last segment and we know there will be some indices remaining afterwards, just tack them on the end.
                // Doing it this way keeps the "numberOfSimultaneousDownloads" value true to its meaning!
                if (i == (numberOfSimultaneousDownloads - 1) && willHaveRemainder)
                {
                    endIndex += fileMapTotal % numberOfSimultaneousDownloads;
                }
                
                segments.Add(DownloadFileMapSegment(fileMap: filesToDownload,
                                                    segmentId: ++segmentTotal,
                                                    startIndex: startIndex,
                                                    endIndex: endIndex));
            }

            await UniTask.WhenAll(segments);

            await UniTask.SwitchToMainThread();
        }


        public static void TallyAllDownloadedSegmentBytes(long amount)
        {
            CurrentDownloadBytesDownloaded += amount;
            CurrentDownloadPercentComplete =  (float) CurrentDownloadBytesDownloaded / FileMapSizeDiff;
        }


        private static async UniTask DownloadFileMapSegment(RemoteS3Object[] fileMap,
                                                            int segmentId,
                                                            int startIndex,
                                                            int endIndex)
        {
//            Debug.LogWarning($"Hi I'm segment [{segmentId}]! I'll download from [{startIndex}] to [{endIndex}]");

            for (var i = startIndex;
                 i <= endIndex;
                 i++)
            {
                Debug.Log($"Segment {segmentId} handling index {i}");

                var entry = fileMap[i];
                
                Debug.Log($"Index {i} is {entry.LocalFilePath}");
                
//                if (entry.IsAFile)
//                {
//                    if (entry.DownloadRequired)
//                    {
                        await entry.DownloadInSegment();
//                    }
//                }
//                else
//                {
//                    if (!Directory.Exists(entry.LocalFilePath))
//                    {
//                        Directory.CreateDirectory(entry.LocalFilePath);
//                    }
//                }
            }
        }

//
//        public static async UniTask FetchDirectories(IEnumerable<FetchableDirectory> directories)
//        {
//            foreach (var d in directories)
//                await FetchDirectory(d.directory,
//                                     d.includeSubdirectories);
//        }

//
//        public static async UniTask FetchDirectory(string s3Directory,
//                                                   bool includeSubdirectories = false)
//        {
//            // The root directory can't feature a lone '/' apparently, but all subdirectories require one.
//            // So if this isn't the root directory and it's missing it's slash, add one
//            if (s3Directory.Length > 0 &&
//                s3Directory[^1]    != '/') s3Directory += '/';
//
//            var remoteObjects = await GetRemoteObjects(s3Directory,
//                                                       includeSubdirectories);
//
//            foreach (var remoteObject in remoteObjects)
//            {
//                // Check if this key is the directory we're already in (yes, it's listed in the XML keys)
//                // It seems to always be the first index, but we shouldn't assume it always will be.
//                if (string.Equals(s3Directory,
//                                  remoteObject.RemoteFilePath,
//                                  StringComparison.Ordinal)) continue;
//
//                if (remoteObject.IsAFile)
//                {
//                    if (remoteObject.DownloadRequired())
//                    {
//                        await remoteObject.Download();
//                    }
//                    else
//                    {
//                        Debug.Log($"File already exists locally at:\n{remoteObject.LocalFilePath}");
//                    }
//                }
//                else
//                {
//                    if (includeSubdirectories)
//                    {
//                        await FetchDirectory(remoteObject.RemoteFilePath,
//                                             includeSubdirectories);
//                    }
//                }
//            }
//        }

        public static async UniTask<RemoteS3Object> GetRemoteObject(string relativePath)
        {
            var listUrl = $"{Remote_List_URL}{UnityWebRequest.EscapeURL(relativePath)}";

            using var listRequest = UnityWebRequest.Get(listUrl);

            await listRequest.SendWebRequest();

            if (listRequest.result is not UnityWebRequest.Result.Success or UnityWebRequest.Result.InProgress)
            {
                Debug.LogError($"Error listing objects: {listRequest.error}");

                return null;
            }

            var xml = new XmlDocument();

            Debug.Log(listRequest.downloadHandler.text);
            
            xml.LoadXml(listRequest.downloadHandler.text);

            var xmlNamespaceManager = new XmlNamespaceManager(xml.NameTable);

            xmlNamespaceManager.AddNamespace(prefix: XML_Namespace_Prefix,
                                             uri: XML_Namespace_Uri);

            var contentsNodes = xml.SelectNodes(xpath: XML_Namespace_XPath,
                                                nsmgr: xmlNamespaceManager);

            if (contentsNodes       == null ||
                contentsNodes.Count == 0) return null;

            var node = contentsNodes[0];

            var keyNode = node.SelectSingleNode(xpath: XML_Node_Key,
                                                nsmgr: xmlNamespaceManager);

            var lastModifiedNode = node.SelectSingleNode(xpath: XML_Node_LastModified,
                                                         nsmgr: xmlNamespaceManager);

            var sizeNode = node.SelectSingleNode(xpath: XML_Node_Size,
                                                 nsmgr: xmlNamespaceManager);

            return new RemoteS3Object(key: keyNode.InnerText,
                                      lastModified: DateTime.Parse(lastModifiedNode.InnerText),
                                      size: long.Parse(sizeNode.InnerText));
        }


        public static async UniTask<List<RemoteS3Object>> GetRemoteObjects(string s3Directory,
                                                                           bool includeSubdirectories = false)
        {
            // Where you're putting s3Directory here, you can put anything! including a specific relative file path.
            // It's treated as a prefix, i.e. flower_ would return flower_shield, flower_whatever, etc
            // You can only search for one prefix per-request.
            Debug.Log(s3Directory);
            
            var listUrl = $"{Remote_List_URL}{UnityWebRequest.EscapeURL(s3Directory)}";

            using var listRequest = UnityWebRequest.Get(listUrl);

            await listRequest.SendWebRequest();

            if (listRequest.result is not UnityWebRequest.Result.Success or UnityWebRequest.Result.InProgress)
            {
                Debug.LogError($"Error listing objects: {listRequest.error}");

                return null;
            }

            var xml = new XmlDocument();

            xml.LoadXml(listRequest.downloadHandler.text);

            var xmlNamespaceManager = new XmlNamespaceManager(xml.NameTable);

            xmlNamespaceManager.AddNamespace(prefix: XML_Namespace_Prefix,
                                             uri: XML_Namespace_Uri);

            var contentsNodes = xml.SelectNodes(xpath: XML_Namespace_XPath,
                                                nsmgr: xmlNamespaceManager);

            if (contentsNodes == null) return null;

            var result = new List<RemoteS3Object>(contentsNodes.Count);

            result.AddRange(
                            from XmlNode contentsNode in contentsNodes
                            let keyNode = contentsNode.SelectSingleNode(xpath: XML_Node_Key,
                                                                        nsmgr: xmlNamespaceManager)
                            let key = keyNode.InnerText
                            let isSubdirectory = key.IndexOf(value: '/',
                                                             startIndex: s3Directory.Length) !=
                                                 -1
                            where !isSubdirectory || includeSubdirectories
                            let sizeNode = contentsNode.SelectSingleNode(xpath: XML_Node_Size,
                                                                         nsmgr: xmlNamespaceManager)
                            let lastModifiedNode = contentsNode.SelectSingleNode(xpath: XML_Node_LastModified,
                                                                                 nsmgr: xmlNamespaceManager)
                            select new RemoteS3Object(key: key,
                                                      lastModified: DateTime.Parse(lastModifiedNode.InnerText),
                                                      size: long.Parse(sizeNode.InnerText)));

//            var what = contentsNodes[0]
//                       .SelectSingleNode(xpath: XML_Node_Key,
//                                         nsmgr: xmlNamespaceManager)
//                       .InnerText;
//            
//            Debug.Log(s3Directory);
//            Debug.Log(what);
//            
//            
//
//            Debug.Log(what.IndexOf(value: '/',
//                                      startIndex: s3Directory.Length) !=
//                      -1);

//
//
//            for (var i = 0;
//                 i < contentsNodes.Count;
//                 i++)
//            {
//                var keyNodeThing = contentsNodes[i]
//                .SelectSingleNode(XML_Node_Key,
//                                  xmlNamespaceManager);
//                
//                Debug.Log(keyNodeThing.InnerText);
//            }
//            
            return result;
        }

//
//        public static async UniTask FetchRelativeFiles(string[] relativePaths)
//        {
//            foreach (var relativePath in relativePaths)
//            {
//                var remoteObject = await GetRemoteObject(relativePath);
//
//                await remoteObject.Download();
//            }
//        }
//        

        private static string RelativeToAbsolute(string relative) => Path.Combine(path1: Remote_Root,
                                                                                  path2: relative)
                                                                         .Replace(oldChar: ' ',
                                                                                  newChar: '+');


        private static string AbsoluteToRelative(string absolute) => Path.GetRelativePath(relativeTo: Remote_Root,
                                                                                          path: absolute)
                                                                         .Replace(oldChar: '+',
                                                                                  newChar: ' ');

    }

}
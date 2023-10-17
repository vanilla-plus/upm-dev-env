#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

    public static class FileSync
    {

        
        private static string Remote_Root     = null;
        private static string Remote_List_URL = null;

        public static string LocalSubDirectory = "fetch";

        private const string XML_Namespace_Uri    = "http://s3.amazonaws.com/doc/2006-03-01/";
        private const string XML_Namespace_Prefix = "s3";

        private const string XML_Namespace_XPath = "//s3:Contents";

        private const string XML_Node_Key          = "s3:Key";
        private const string XML_Node_Size         = "s3:Size";
        private const string XML_Node_LastModified = "s3:LastModified";

//        public static int Download_Chunk_Buffer_ByteSize = 64 * 1024;         // 64 kb
//        public static int Download_Chunk_Buffer_ByteSize = 8  * 1024 * 1024;  // 8  mb
//        public static int Download_Chunk_Buffer_ByteSize = 16 * 1024 * 1024;  // 16 mb
        public static int Download_Chunk_Buffer_ByteSize = 32 * 1024 * 1024;  // 32 mb
//        public static int Download_Chunk_Buffer_ByteSize = 64 * 1024 * 1024;  // 64 mb

        public static long  BytesDownloaded = 0;
        public static long  BytesTotal      = 0;
        public static float BytesPercent    = 0.0f;

//        public static Action<float> OnDownloadProgress;

//        private const string XML_Node_Etag         = "s3:ETag";
//        private const string XML_Node_StorageClass = "s3:StorageClass";

        public static void Initialize(string remoteRoot)
        {
            Remote_Root     = remoteRoot;
            Remote_List_URL = $"{Remote_Root}?list-type=2&prefix=";
        }


        public static async UniTask<RemoteS3Object[]> GetFileMap(IEnumerable<FetchableDirectory> directories,
                                                                 IEnumerable<string> files)
        {
            var fileMap = new HashSet<RemoteS3Object>();

            foreach (var entry in directories)
            {
                var remoteObjects = await GetRemoteObjects(entry.directory,
                                                           entry.includeSubdirectories);

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

            #if debug
            Debug.Log("FileMap results:");
            foreach (var f in fileMap) Debug.Log(f.RemoteFilePath);
            #endif

            return fileMap.ToArray();
        }
        
        public static async UniTask SynchronizeFileMap(RemoteS3Object[] fileMap, int numberOfSimultaneousDownloads)
        {
            BytesDownloaded = 0;
            BytesTotal      = 0;
            
            // We have to create the directories first, otherwise we may have files trying to be created in them
            // before they exist and that leads to an error.
            
            // ToDo - Handle directory-matching outside of this function altogether.
            foreach (var f in fileMap)
            {
                if (f.IsAFile)
                {
                    BytesTotal += f.RemoteFileSize;
                }
                else
                {
                    if (!Directory.Exists(f.LocalFilePath))
                    {
                        Directory.CreateDirectory(f.LocalFilePath);
                    }
                }
            }
            
            numberOfSimultaneousDownloads = Math.Clamp(numberOfSimultaneousDownloads,
                                                       1,
                                                       8);
            
            int fileMapTotal         = fileMap.Length;
            int fileMapSegmentStride = fileMapTotal                  / numberOfSimultaneousDownloads; // How many indices should each segment be responsible for?
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
                
                segments.Add(DownloadFileMapSegment(fileMap,
                                                    ++segmentTotal,
                                                    startIndex,
                                                    endIndex));
            }

            await UniTask.WhenAll(segments);

            await UniTask.SwitchToMainThread();
        }


        public static void TallyBytes(long amount)
        {
            BytesDownloaded += amount;
            BytesPercent    =  (float) BytesDownloaded / BytesTotal;

//            var progress = (float) BytesDownloaded / BytesTotal;
            
//            Debug.Log(progress);
            
//            OnDownloadProgress?.Invoke(progress);
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
                
                if (entry.IsAFile)
                {
                    if (entry.DownloadRequired())
                    {
                        await entry.Download();
                    }
                }
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

            xml.LoadXml(listRequest.downloadHandler.text);

            var xmlNamespaceManager = new XmlNamespaceManager(xml.NameTable);

            xmlNamespaceManager.AddNamespace(XML_Namespace_Prefix,
                                             XML_Namespace_Uri);

            var contentsNodes = xml.SelectNodes(XML_Namespace_XPath,
                                                xmlNamespaceManager);

            if (contentsNodes       == null ||
                contentsNodes.Count == 0) return null;

            var node = contentsNodes[0];

            var keyNode = node.SelectSingleNode(XML_Node_Key,
                                                xmlNamespaceManager);

            var lastModifiedNode = node.SelectSingleNode(XML_Node_LastModified,
                                                         xmlNamespaceManager);

            var sizeNode = node.SelectSingleNode(XML_Node_Size,
                                                 xmlNamespaceManager);

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

            xmlNamespaceManager.AddNamespace(XML_Namespace_Prefix,
                                             XML_Namespace_Uri);

            var contentsNodes = xml.SelectNodes(XML_Namespace_XPath,
                                                xmlNamespaceManager);

            if (contentsNodes == null) return null;

            var result = new List<RemoteS3Object>(contentsNodes.Count);

            result.AddRange(
                            from XmlNode contentsNode in contentsNodes
                            let keyNode = contentsNode.SelectSingleNode(XML_Node_Key,
                                                                        xmlNamespaceManager)
                            let key = keyNode.InnerText
                            let isSubdirectory = key.IndexOf('/',
                                                             s3Directory.Length) !=
                                                 -1
                            where !isSubdirectory || includeSubdirectories
                            let sizeNode = contentsNode.SelectSingleNode(XML_Node_Size,
                                                                         xmlNamespaceManager)
                            let lastModifiedNode = contentsNode.SelectSingleNode(XML_Node_LastModified,
                                                                                 xmlNamespaceManager)
                            select new RemoteS3Object(key: key,
                                                      lastModified: DateTime.Parse(lastModifiedNode.InnerText),
                                                      size: long.Parse(sizeNode.InnerText)));

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

        private static string RelativeToAbsolute(string relative) => Path.Combine(Remote_Root,
                                                                                  relative)
                                                                         .Replace(oldChar: ' ',
                                                                                  newChar: '+');


        private static string AbsoluteToRelative(string absolute) => Path.GetRelativePath(Remote_Root,
                                                                                          absolute)
                                                                         .Replace(oldChar: '+',
                                                                                  newChar: ' ');


        [Serializable]
        public class RemoteS3Object
        {

            [SerializeField] public readonly bool     IsAFile;
            [SerializeField] public readonly string   LocalFilePath;
            [SerializeField] public readonly string   RemoteFilePath;
            [SerializeField] public readonly DateTime RemoteLastModified;
            [SerializeField] public readonly long     RemoteFileSize;

            public RemoteS3Object() { }


            public RemoteS3Object(string key,
                                  DateTime lastModified,
                                  long size)
            {
                IsAFile = key.Length > 0 && key[^1] != '/';

                LocalFilePath = Path.Combine(Application.persistentDataPath,
                                             LocalSubDirectory,
                                             key);

                RemoteFilePath     = key;
                RemoteLastModified = lastModified;
                RemoteFileSize     = size;
            }
            
            public override bool Equals(object obj)
            {
                // Check if obj is null or not of type RemoteS3Object
                if (obj is RemoteS3Object other)
                {
                    // Compare the RemoteFilePath properties for equality
                    return RemoteFilePath == other.RemoteFilePath;
                }
                return false;
            }

            // Use the hash code of the RemoteFilePath property
            public override int GetHashCode() => RemoteFilePath != null ? RemoteFilePath.GetHashCode() : 0;

            public override string ToString() => $"RemoteS3Object Log\nIsAFile\t[{IsAFile}]\nKey\t[{RemoteFilePath}]\nLastModified\t[{RemoteLastModified}]\nSize\t[{RemoteFileSize}]";


            public bool DownloadRequired()
            {
                if (!File.Exists(LocalFilePath))
                {
                    #if debug
                    Debug.Log($"File [{RemoteFilePath}] doesn't exist locally - download approved");
                    #endif
                    
                    return true;
                }

                var localFileInfo = new FileInfo(LocalFilePath);

                // Compare the file sizes
                var isSizeDifferent = localFileInfo.Length != RemoteFileSize;

                if (isSizeDifferent)
                {
                    #if debug
                    Debug.Log($"File size for [{RemoteFilePath}] doesn't match local file - download approved");
                    #endif
                    
                    // File needs sync if either size or last modified timestamp is different
                    return true;
                }

                // ToDo - This is unreliable and needs proper investigating (it's 5:45am gimme a break)
                // ToDo - The REALLY correct way would be to use eTags.
                // ToDo - write the eTag string to a meta data file for each asset
                // ToDo - And then check the tag before each download.
                
//                // Compare the last modified timestamps
//                var isLastModifiedDifferent = localFileInfo.LastWriteTimeUtc != RemoteLastModified.ToUniversalTime();
//                
//                if (isLastModifiedDifferent)
//                {
//                    Debug.Log($"File modification date for [{RemoteFilePath}] doesn't match local file - download approved");
//
//                    Debug.Log(RemoteLastModified.ToUniversalTime().ToString());
//                    Debug.Log(localFileInfo.LastWriteTimeUtc.ToString());
//                    
//                    // File needs sync if either size or last modified timestamp is different
//                    return true;
//                }

                return false;
            }

//
//            public async UniTask Download(Func<float, float> OnProgress = null)
//            {
//                Debug.Log($"Download started for [{RemoteFilePath}]");
//                
//                var absoluteRemotePath = RelativeToAbsolute(RemoteFilePath);
//
//                using var fileRequest = UnityWebRequest.Get(absoluteRemotePath);
//
//                fileRequest.downloadHandler = new DownloadHandlerBuffer();
//
//                var op = fileRequest.SendWebRequest();
//
//                while (!op.isDone)
//                {
//                    OnProgress?.Invoke(op.progress);
//                    
//                    await UniTask.Yield();
//                }
//
//                OnProgress?.Invoke(op.progress);
//
//                if (fileRequest.result == UnityWebRequest.Result.Success)
//                {
//                    // If the directory this file would exist in doesn't exist yet, create it.
//
//                    var targetDirectory = Path.GetDirectoryName(LocalFilePath);
//
//                    if (targetDirectory != null) Directory.CreateDirectory(targetDirectory);
//
//                    if (fileRequest.downloadHandler.data == null)
//                    {
//                        Debug.LogError("Fetched data was null. Did you accidentally try to download a directory using a file operation?");
//                    }
//                    else
//                    {
//                        await File.WriteAllBytesAsync(LocalFilePath,
//                                                      fileRequest.downloadHandler.data);
//                    }
//                }
//                else
//                {
//                    Debug.LogError($"Error downloading file: {fileRequest.error}");
//                }
//                
//                fileRequest.downloadHandler?.Dispose();
//                fileRequest?.Dispose();
//            }
//
            public async UniTask Download(Func<float, float> OnProgress = null)
            {
                try
                {
                    var absoluteRemotePath = RelativeToAbsolute(RemoteFilePath);

                    using var client = new HttpClient();

                    using var response = await client.GetAsync(absoluteRemotePath,
                                                               HttpCompletionOption.ResponseHeadersRead);

                    await using var httpStream = await response.Content.ReadAsStreamAsync();

                    await using var fileStream = new FileStream(LocalFilePath,
                                                                FileMode.Create);

                    var buffer = new byte[Download_Chunk_Buffer_ByteSize];
                    
                    int bytesRead;

//                    long totalBytesRead = 0;

//                    while (Application.isPlaying && (bytesRead = await httpStream.ReadAsync(buffer, 
                    while ((bytesRead = await httpStream.ReadAsync(buffer,
                                                                   0,
                                                                   buffer.Length)) >
                           0)
                    {
//                        totalBytesRead += bytesRead;

//                        var progress = (float) totalBytesRead / RemoteFileSize;

//                        OnProgress?.Invoke(progress);

//                        Debug.Log(progress);

                        TallyBytes(bytesRead);
                        
                        await fileStream.WriteAsync(buffer,
                                                    0,
                                                    bytesRead);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }

        }

    }

}
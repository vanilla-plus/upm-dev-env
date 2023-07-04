using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

namespace Vanilla.S3
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
//        private const string XML_Node_Etag         = "s3:ETag";
//        private const string XML_Node_StorageClass = "s3:StorageClass";


        public static void Initialize(string region,
                                      string bucket)
        {
            Remote_Root     = $"https://{bucket}.s3.{region}.amazonaws.com/";
            Remote_List_URL = $"{Remote_Root}?list-type=2&prefix=";

//        Debug.Log("S3Sync initialized");
        }


        public static async UniTask FetchDirectories(IEnumerable<FetchableDirectory> directories)
        {
            foreach (var d in directories)
                await FetchDirectory(d.directory,
                                     d.includeSubdirectories);
        }


        public static async UniTask FetchDirectory(string s3Directory,
                                                   bool includeSubdirectories = false)
        {
//            Debug.LogError(s3Directory);
//            if (s3Directory.Length == 0 || s3Directory[^1] != '/') s3Directory += '/';

            // The root directory can't feature a lone '/' apparently, but all subdirectories require one.
            // So if this isn't the root directory and it's missing it's slash, add one
            if (s3Directory.Length > 0 && s3Directory[^1] != '/') s3Directory += '/';

//            Debug.LogError("You should see this once per-serialized-path");

            var remoteObjects = await GetRemoteObjects(s3Directory,
                                                   includeSubdirectories);

//            Debug.LogError(remoteObjects.Count);
            foreach (var o in remoteObjects) Debug.LogError(o.ToString());
//            Debug.LogError(s3Directory);

            // If the last character isn't a /, add one.
//            if (s3Directory.Length == 0 || s3Directory[^1] != '/') s3Directory += '/';
//            Debug.LogError(s3Directory);

            foreach (var remoteObject in remoteObjects)
            {
                // Check if this key is the directory we're already in (yes, it's listed in the XML keys)
                // It seems to always be the first index, but we shouldn't assume it always will be.
                if (string.Equals(s3Directory,
                                  remoteObject.RemoteFilePath,
                                  StringComparison.Ordinal)) continue;

//            Debug.Log($"{s3Directory} is not {key}");

                // If the key ends with /, it's a subdirectory.
                // In that case, recurse down.
//            if (key.EndsWith('/'))
//                if (Path.HasExtension(remoteObject.RemoteFilePath))
                if (remoteObject.IsAFile)
                {
//                    Debug.Log($"{remoteObject.RemoteFilePath} is a File!");

                    if (remoteObject.DownloadRequired())
                    {
//                        await FetchRelativeFile(remoteObject.RemoteFilePath);

                        await remoteObject.Download();
                    }
                    else
                    {
                        Debug.Log($"File already exists locally at:\n{remoteObject.LocalFilePath}");
                    }
                }
                else
                {
//                Debug.Log($"{key} is a Subdirectory!");

                    if (includeSubdirectories)
                    {
//                    Debug.LogWarning("Recursion allowed!");

                        await FetchDirectory(remoteObject.RemoteFilePath,
                                             includeSubdirectories);
                    }
                    else
                    {
//                    Debug.LogWarning("Recursion blocked");
                    }
                }
            }
        }

//
//        public static async UniTask<string[]> ListDirectoryContents(FetchableDirectory fetchableDirectory) => await ListDirectoryContents(fetchableDirectory.directory,
//                                                                                                                                          fetchableDirectory.includeSubdirectories);

//
//        public static async UniTask<string[]> ListDirectoryContents(string s3Directory,
//                                                                    bool includeSubdirectories = false)
//        {
//            var listUrl = $"{Remote_List_URL}{UnityWebRequest.EscapeURL(s3Directory)}";
//
//            using var listRequest = UnityWebRequest.Get(listUrl);
//
//            await listRequest.SendWebRequest();
//
//            if (listRequest.result is not UnityWebRequest.Result.Success or UnityWebRequest.Result.InProgress)
//            {
//                Debug.LogError($"Error listing objects: {listRequest.error}");
//
//                return null;
//            }
//
//            var xml = new XmlDocument();
//
//            xml.LoadXml(listRequest.downloadHandler.text);
//
//            var xmlNamespaceManager = new XmlNamespaceManager(xml.NameTable);
//
//            xmlNamespaceManager.AddNamespace(XML_Namespace_Prefix,
//                                             XML_Namespace_Uri);
//
//            var keys = xml.SelectNodes(XML_Namespace_XPath,
//                                       xmlNamespaceManager);
//
//            return keys?.Cast<XmlNode>().Select(keyNode => keyNode.InnerText).Where(key => includeSubdirectories || key.LastIndexOf('/') == s3Directory.Length - 1).ToArray();
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


        public static async UniTask<List<RemoteS3Object>> GetRemoteObjects(string s3Directory, bool includeSubdirectories = false)
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

            // This is the point where we should filter using includeSubdirectories...
//            var nodes2 = contentsNodes?.Cast<XmlNode>().Select(keyNode => keyNode.InnerText).Where(key => includeSubdirectories || key.LastIndexOf('/') == s3Directory.Length - 1);

            if (contentsNodes == null) return null;
//
//            var result = new List<RemoteS3Object>(contentsNodes.Count);
//
//            result.AddRange(
//                            from XmlNode contentsNode in contentsNodes
//                            let keyNode = contentsNode.SelectSingleNode(XML_Node_Key,
//                                                                        xmlNamespaceManager)
////                            let lastModifiedNode = contentsNode.SelectSingleNode(XML_Node_LastModified,
////                                                                                 xmlNamespaceManager)
////                            let eTagNode = contentsNode.SelectSingleNode(XML_Node_Etag,
////                                                                         xmlNamespaceManager)
//                            let sizeNode = contentsNode.SelectSingleNode(XML_Node_Size,
//                                                                         xmlNamespaceManager)
////                            let storageClassNode = contentsNode.SelectSingleNode(XML_Node_StorageClass,
////                                                                                 xmlNamespaceManager)
//                            select new RemoteS3Object(key: keyNode.InnerText,
//
////                                                      lastModified: DateTime.Parse(lastModifiedNode.InnerText),
////                                                      eTag: eTagNode.InnerText.Trim('"'),
////                                                      size: long.Parse(sizeNode.InnerText),
//                                                      size: long.Parse(sizeNode.InnerText)));
////                                                      storageClass: storageClassNode.InnerText));
////
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
//                                                      eTag: eTagNode.InnerText.Trim('"'),
//                                                      size: long.Parse(sizeNode.InnerText),
                                                      size: long.Parse(sizeNode.InnerText)
//                                                      storageClass: storageClassNode.InnerText
                                                     ));

            return result;
        }


        public static async UniTask FetchAbsoluteFiles(string[] urls)
        {
            foreach (var url in urls) await FetchAbsoluteFile(url);
        }


        public static async UniTask FetchAbsoluteFile(string absolute)
        {
            var relative = AbsoluteToRelative(absolute);

            var localFilePath = Path.Combine(Application.persistentDataPath,
                                             LocalSubDirectory,
                                             relative);

            if (File.Exists(localFilePath))
            {
                Debug.LogWarning($"File already exists [{localFilePath}]");

                return;
            }

//        Debug.Log($"Attempting download:\n[{absolute}]\n[{localFilePath}]");

            using var fileRequest = UnityWebRequest.Get(absolute);

            fileRequest.downloadHandler = new DownloadHandlerFile(localFilePath);

            await fileRequest.SendWebRequest();

//        Debug.Log($"{fileRequest.result.ToString()}");
        }


        public static async UniTask FetchRelativeFiles(string[] urls)
        {
            foreach (var url in urls) await FetchRelativeFile(url);
        }


//    public static async UniTask FetchRelativeFile(string relative)
//    {
//        var remoteFilePath = RelativeToAbsolute(relative);
//
//        var localFilePath = Path.Combine(Application.persistentDataPath,
//                                         relative);
//
//        if (File.Exists(localFilePath))
//        {
//            Debug.LogWarning($"File already exists [{localFilePath}]");
//
//            return;
//        }
//
//        Debug.Log($"Attempting download:\n[{remoteFilePath}]\n[{localFilePath}]");
//
//        using var fileRequest = UnityWebRequest.Get(remoteFilePath);
//
//        fileRequest.downloadHandler = new DownloadHandlerFile(localFilePath);
//
//        await fileRequest.SendWebRequest();
//
//        Debug.Log($"{fileRequest.result.ToString()}");
//    }


        public static async UniTask FetchRelativeFile(string relative)
        {
            var remoteFilePath = RelativeToAbsolute(relative);

            var localFilePath = Path.Combine(Application.persistentDataPath,
                                             LocalSubDirectory,
                                             relative);

            if (File.Exists(localFilePath))
            {
//                FileInfo localFileInfo = new FileInfo(localFilePath);
//                long     localFileSize = localFileInfo.Length; // Size in bytes
//
//                // Compare the sizes
//                return localFileSize == remoteFileSize;
                
                Debug.LogWarning($"File already exists [{localFilePath}]");

                return;
            }

//        Debug.Log($"Attempting download:\n[{remoteFilePath}]\n[{localFilePath}]");

            using var fileRequest = UnityWebRequest.Get(remoteFilePath);

            fileRequest.downloadHandler = new DownloadHandlerBuffer();

            // We can't simply await this because it can't handle errors properly.
            //        await fileRequest.SendWebRequest();

            var op = fileRequest.SendWebRequest();

            while (!op.isDone)
            {
                await UniTask.Yield();
            }

            if (fileRequest.result == UnityWebRequest.Result.Success)
            {
                // If the directory this file would exist in doesn't exist yet, create it.

                var targetDirectory = Path.GetDirectoryName(localFilePath);

                if (targetDirectory != null) Directory.CreateDirectory(targetDirectory);

                if (fileRequest.downloadHandler.data == null)
                {
                    Debug.LogError("Fetched data was null. Did you accidentally try to download a directory using a file operation?");
                }
                else
                {
                    await File.WriteAllBytesAsync(localFilePath,
                                                  fileRequest.downloadHandler.data);
                }
            }
            else
            {
                Debug.LogError($"Error downloading file: {fileRequest.error}");
            }
        }


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

        [SerializeField] public bool     IsAFile;
        [SerializeField] public string   LocalFilePath;
        [SerializeField] public string   RemoteFilePath;
        [SerializeField] public DateTime RemoteLastModified;
        [SerializeField] public long     RemoteFileSize;

        public RemoteS3Object() { }


        public RemoteS3Object(string key,
                              DateTime lastModified,
                              long size)
        {
            IsAFile = key[^1] != '/';

            LocalFilePath = Path.Combine(Application.persistentDataPath,
                                         LocalSubDirectory,
                                         key);

            RemoteFilePath     = key;
            RemoteLastModified = lastModified;
            RemoteFileSize     = size;
        }


//        public override string ToString() => $"RemoteS3Object Log\nIsAFile\t[{IsAFile}]\nKey\t[{Key}]\nLastModified\t[{LastModified}]\nETag\t[{ETag}]\nSize\t[{Size}]\nStorageClass\t[{StorageClass}]";
//        public override string ToString() => $"RemoteS3Object Log\nIsAFile\t[{IsAFile}]\nKey\t[{Key}]\nSize\t[{RemoteFileSize}]";
        public override string ToString() => $"RemoteS3Object Log\nIsAFile\t[{IsAFile}]\nKey\t[{RemoteFilePath}]\nLastModified\t[{RemoteLastModified}]\nSize\t[{RemoteFileSize}]";

        public bool DownloadRequired()
        {
            if (!File.Exists(LocalFilePath)) return true;
            
            var localFileInfo = new FileInfo(LocalFilePath);

            // Compare the file sizes
            var isSizeDifferent = localFileInfo.Length != RemoteFileSize;

            // Compare the last modified timestamps
            var isLastModifiedDifferent = localFileInfo.LastWriteTimeUtc != RemoteLastModified.ToUniversalTime();

            // File needs sync if either size or last modified timestamp is different
            return isSizeDifferent || isLastModifiedDifferent;
        }
        
        public async UniTask Download()
        {
            var absoluteRemotePath = RelativeToAbsolute(RemoteFilePath);

//        Debug.Log($"Attempting download:\n[{remoteFilePath}]\n[{localFilePath}]");

            using var fileRequest = UnityWebRequest.Get(absoluteRemotePath);

            fileRequest.downloadHandler = new DownloadHandlerBuffer();

            // We can't simply await this because it can't handle errors properly.
//            try
//            {
//                await fileRequest.SendWebRequest();
//            }
//            catch (Exception e)
//            {
//                Debug.LogException(e);
//            }

            var op = fileRequest.SendWebRequest();

            while (!op.isDone) await UniTask.Yield();

            if (fileRequest.result == UnityWebRequest.Result.Success)
            {
                // If the directory this file would exist in doesn't exist yet, create it.

                var targetDirectory = Path.GetDirectoryName(LocalFilePath);

                if (targetDirectory != null) Directory.CreateDirectory(targetDirectory);

                if (fileRequest.downloadHandler.data == null)
                {
                    Debug.LogError("Fetched data was null. Did you accidentally try to download a directory using a file operation?");
                }
                else
                {
                    await File.WriteAllBytesAsync(LocalFilePath,
                                                  fileRequest.downloadHandler.data);
                }
            }
            else
            {
                Debug.LogError($"Error downloading file: {fileRequest.error}");
            }
        }
        
    }
        
    }

}
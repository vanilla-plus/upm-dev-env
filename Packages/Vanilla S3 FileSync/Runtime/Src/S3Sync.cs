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
    
    // This will be useful after shipping.
    // You could tally the byte total and then present a progress meter to the user.
    
//    [Serializable]
//    public class RemoteS3Object
//    {
//        public string   Key          { get; set; }
//        public DateTime LastModified { get; set; }
//        public string   ETag         { get; set; }
//        public long     Size         { get; set; }
//        public string   StorageClass { get; set; }
//    }

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

        private const string XML_Namespace_XPath   = "//s3:Contents/s3:Key";
        
//        private const string XML_Node_Key          = "s3:Key";
//        private const string XML_Node_Size         = "s3:Size";
//        private const string XML_Node_LastModified = "s3:LastModified";
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
//        Debug.Log(s3Directory);

            var keys = await ListDirectoryContents(s3Directory,
                                                   includeSubdirectories);

            // If the last character isn't a /, add one.
            if (s3Directory.Length == 0 || s3Directory[^1] != '/') s3Directory += '/';

            foreach (var key in keys)
            {
                // Check if this key is the directory we're already in (yes, it's listed in the XML keys)
                // It seems to always be the first index, but we shouldn't assume it always will be.
                if (string.Equals(s3Directory,
                                  key,
                                  StringComparison.Ordinal)) continue;

//            Debug.Log($"{s3Directory} is not {key}");

                // If the key ends with /, it's a subdirectory.
                // In that case, recurse down.
//            if (key.EndsWith('/'))
                if (Path.HasExtension(key))
                {
//                Debug.Log($"{key} is a File!");

                    await FetchRelativeFile(key);
                }
                else
                {
//                Debug.Log($"{key} is a Subdirectory!");

                    if (includeSubdirectories)
                    {
//                    Debug.LogWarning("Recursion allowed!");

                        await FetchDirectory(key,
                                             includeSubdirectories);
                    }
                    else
                    {
//                    Debug.LogWarning("Recursion blocked");
                    }
                }
            }
        }


        public static async UniTask<string[]> ListDirectoryContents(FetchableDirectory fetchableDirectory) => await ListDirectoryContents(fetchableDirectory.directory,
                                                                                                                                          fetchableDirectory.includeSubdirectories);


        public static async UniTask<string[]> ListDirectoryContents(string s3Directory,
                                                                    bool includeSubdirectories = false)
        {
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

            var keys = xml.SelectNodes(XML_Namespace_XPath,
                                       xmlNamespaceManager);

            return keys?.Cast<XmlNode>().Select(keyNode => keyNode.InnerText).Where(key => includeSubdirectories || key.LastIndexOf('/') == s3Directory.Length - 1).ToArray();
        }

//
//        public static async UniTask<List<RemoteS3Object>> ListDirectoryContents(string s3Directory)
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
//            var contentsNodes = xml.SelectNodes(XML_Namespace_XPath,
//                                                xmlNamespaceManager);
//
//            if (contentsNodes == null) return null;
//
//            var result = new List<RemoteS3Object>(contentsNodes.Count);
//
//            result.AddRange(
//                            from XmlNode contentsNode in contentsNodes
//                            let keyNode = contentsNode.SelectSingleNode(XML_Node_Key,
//                                                                        xmlNamespaceManager)
//                            let lastModifiedNode = contentsNode.SelectSingleNode(XML_Node_LastModified,
//                                                                                 xmlNamespaceManager)
//                            let eTagNode = contentsNode.SelectSingleNode(XML_Node_Etag,
//                                                                         xmlNamespaceManager)
//                            let sizeNode = contentsNode.SelectSingleNode(XML_Node_Size,
//                                                                         xmlNamespaceManager)
//                            let storageClassNode = contentsNode.SelectSingleNode(XML_Node_StorageClass,
//                                                                                 xmlNamespaceManager)
//                            select new RemoteS3Object
//                                   {
//                                       Key          = keyNode.InnerText,
//                                       LastModified = DateTime.Parse(lastModifiedNode.InnerText),
//                                       ETag         = eTagNode.InnerText.Trim('"'),
//                                       Size         = long.Parse(sizeNode.InnerText),
//                                       StorageClass = storageClassNode.InnerText
//                                   });
//
//            return result;
//        }


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

    }

}
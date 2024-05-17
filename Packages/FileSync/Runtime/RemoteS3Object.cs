using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.FileSync
{

	public static partial class FileSync
	{

		[Serializable]
		public class RemoteS3Object
		{

			[SerializeField] public bool     IsAFile;
			[SerializeField] public string   FileName;
			[SerializeField] public string   FileExtension;
			[SerializeField] public string   LocalFilePath;
			[SerializeField] public string   RelativeRemoteFilePath;
			[SerializeField] public string   AbsoluteRemoteFilePath;
			[SerializeField] public DateTime RemoteLastModified;
			[SerializeField] public long     RemoteFileSize;
			[SerializeField] public long     LocalFileSize;
			[SerializeField] public bool     DownloadRequired;

			public RemoteS3Object() { }


			public RemoteS3Object(string key,
			                      DateTime lastModified,
			                      long size)
			{
				// This worked but isn't it overkill?
//                IsAFile = key.Length > 0 && key[^1] != '/';
                
                

//                Debug.Log($"[{key}] is {(IsAFile ? "a file" : "not a file")}");

//				LocalFilePath = Local_Path_Segments_To_Skip > 0 ?
//					                Path.Combine(path1: Application.persistentDataPath,
//					                             path2: Local_Root,
//					                             path3: string.Join(separator: '/',
//					                                                values: key.Split('/').Skip(Local_Path_Segments_To_Skip))) :
//					                Path.Combine(path1: Application.persistentDataPath,
//					                             path2: Local_Root,
//					                             path3: key);
                
				// Let's not assume that Files will end up in persistent data path.
				// They most likely will and foregoing this will require the creation of a type of StringSource_Concatenation
				// Purely so we can have Local_Root default to it with StringSource_ApplicationPersistentDataPath in the first slot and the second slot a direct 'fs'
				LocalFilePath = Local_Path_Segments_To_Skip > 0 ?
					                Path.Combine(path1: Local_Root,
					                             path2: string.Join(separator: '/',
					                                                values: key.Split('/').Skip(Local_Path_Segments_To_Skip))) :
					                Path.Combine(path1: Local_Root,
					                             path2: key);

				IsAFile       = size > 0;
				FileName      = Path.GetFileNameWithoutExtension(key);
				FileExtension = Path.GetExtension(key);

				RelativeRemoteFilePath = key;
				AbsoluteRemoteFilePath = RelativeToAbsolute(key);
				RemoteLastModified     = lastModified;
				RemoteFileSize         = size;

				if (!IsAFile)
				{
					DownloadRequired = false;
                    
					if (!Directory.Exists(LocalFilePath)) Directory.CreateDirectory(LocalFilePath);
				}
				else
				{
					if (!File.Exists(LocalFilePath))
					{
						#if debug
						Debug.LogWarning($"File [{RelativeRemoteFilePath}] doesn't exist locally - download approved");
						#endif

						DownloadRequired = true;
					}
					else
					{
						var localFileInfo = new FileInfo(LocalFilePath);

						LocalFileSize = localFileInfo.Length;

//                    Debug.Log($"LocalFile [{RemoteFilePath}] Size [{LocalFileSize}]");

						DownloadRequired = localFileInfo.Length != RemoteFileSize;

						if (DownloadRequired)
						{
							#if debug
							Debug.LogWarning($"Remote file size [{RemoteFileSize}] for [{RelativeRemoteFilePath}] doesn't match local file size [{LocalFileSize}] - download approved");
							#endif
						}
					}
				}
                
                
			}


			public override bool Equals(object obj)
			{
				// Check if obj is null or not of type RemoteS3Object
				if (obj is RemoteS3Object other)
				{
					// Compare the RemoteFilePath properties for equality
					return RelativeRemoteFilePath == other.RelativeRemoteFilePath;
				}
				return false;
			}

			// Use the hash code of the RemoteFilePath property
			public override int GetHashCode() => RelativeRemoteFilePath != null ? RelativeRemoteFilePath.GetHashCode() : 0;

			public override string ToString() => $"RemoteS3Object Log\nIsAFile\t[{IsAFile}]\nKey\t[{RelativeRemoteFilePath}]\nLastModified\t[{RemoteLastModified}]\nSize\t[{RemoteFileSize}]";

//
//            public bool DownloadRequired()
//            {
//                if (!File.Exists(LocalFilePath))
//                {
//                    #if debug
//                    Debug.Log($"File [{RemoteFilePath}] doesn't exist locally - download approved");
//                    #endif
//                    
//                    return true;
//                }
//
//                var localFileInfo = new FileInfo(LocalFilePath);
//
//                // Compare the file sizes
//                var isSizeDifferent = localFileInfo.Length != RemoteFileSize;
//
//                if (isSizeDifferent)
//                {
//                    #if debug
//                    Debug.Log($"File size for [{RemoteFilePath}] doesn't match local file - download approved");
//                    #endif
//                    
//                    // File needs sync if either size or last modified timestamp is different
//                    return true;
//                }
//
//                // ToDo - This is unreliable and needs proper investigating (it's 5:45am gimme a break)
//                // ToDo - The REALLY correct way would be to use eTags.
//                // ToDo - write the eTag string to a meta data file for each asset
//                // ToDo - And then check the tag before each download.
//                
////                // Compare the last modified timestamps
////                var isLastModifiedDifferent = localFileInfo.LastWriteTimeUtc != RemoteLastModified.ToUniversalTime();
////                
////                if (isLastModifiedDifferent)
////                {
////                    Debug.Log($"File modification date for [{RemoteFilePath}] doesn't match local file - download approved");
////
////                    Debug.Log(RemoteLastModified.ToUniversalTime().ToString());
////                    Debug.Log(localFileInfo.LastWriteTimeUtc.ToString());
////                    
////                    // File needs sync if either size or last modified timestamp is different
////                    return true;
////                }
//
//                return false;
//            }

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
			public async UniTask DownloadInSegment(Func<float, float> OnProgress = null)
			{
				try
				{
//					var absoluteRemotePath = RelativeToAbsolute(RelativeRemoteFilePath);

					using var client = new HttpClient();

					using var response = await client.GetAsync(requestUri: AbsoluteRemoteFilePath,
					                                           completionOption: HttpCompletionOption.ResponseHeadersRead);

					await using var httpStream = await response.Content.ReadAsStreamAsync();

					await using var fileStream = new FileStream(path: LocalFilePath,
					                                            mode: FileMode.Create);

					var buffer = new byte[Download_Chunk_Buffer_ByteSize];
                    
					int bytesRead;

					while ((bytesRead = await httpStream.ReadAsync(buffer: buffer,
					                                               offset: 0,
					                                               count: buffer.Length)) >
					       0)
					{
						TallyAllDownloadedSegmentBytes(bytesRead);
                        
						await fileStream.WriteAsync(buffer: buffer,
						                            offset: 0,
						                            count: bytesRead);
					}
				}
				catch (Exception e)
				{
					Debug.LogException(e);
				}
			}


			public async UniTask<byte[]> DownloadBytes(Func<float, float> OnProgress = null, CancellationToken cancellationToken = default)
			{
				try
				{
//					var absoluteRemotePath = RelativeToAbsolute(RelativeRemoteFilePath);

					using var response = await HTTPClient.GetAsync(requestUri: AbsoluteRemoteFilePath,
					                                                        completionOption: HttpCompletionOption.ResponseHeadersRead,
					                                                        cancellationToken: cancellationToken).ConfigureAwait(false);

					await using var httpStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

					await using var memoryStream = new MemoryStream((int)RemoteFileSize);

					var buffer = new byte[Download_Chunk_Buffer_ByteSize];
        
					int  bytesRead;
					long totalBytesRead = 0;

					while ((bytesRead = await httpStream.ReadAsync(buffer: buffer,
					                                               offset: 0,
					                                               count: buffer.Length,
					                                               cancellationToken: cancellationToken).ConfigureAwait(false)) > 0)
					{
						totalBytesRead += bytesRead;
						
						var progress = (float)totalBytesRead / RemoteFileSize;

						#if debug
						Debug.Log($"[{RelativeRemoteFilePath}] Download progress [{progress *100.0f:F1}%]");
						#endif

						OnProgress?.Invoke(progress);
            
						await memoryStream.WriteAsync(buffer: buffer,
						                              offset: 0,
						                              count: bytesRead,
						                              cancellationToken: cancellationToken).ConfigureAwait(false);
					}

					return memoryStream.ToArray();
				}
				catch (Exception e)
				{
					Debug.LogException(e);
					return null;
				}
			}
            
		}

	}

}
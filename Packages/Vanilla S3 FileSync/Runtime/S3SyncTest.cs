using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.S3
{

	public class S3SyncTest : MonoBehaviour
	{

		public string region = "ap-southeast-2";
		public string bucket = string.Empty;

		public string[]             testFiles;
		public FetchableDirectory[] testDirectories;


		public void Start() => FileSync.Initialize(region,
		                                           bucket);


//		[ContextMenu("Fetch File")]
//		public void FetchFile() => FileSync.FetchRelativeFile(testFiles[0]).Forget();


//		[ContextMenu("Fetch Files")]
//		public void FetchFiles() => FileSync.FetchRelativeFiles(testFiles).Forget();
//
//
//		[ContextMenu("Fetch Directory")]
//		public void FetchDirectory() => FileSync.FetchDirectory(testDirectories[0].directory,
//		                                                        testDirectories[0].includeSubdirectories)
//		                                        .Forget();
//
//
//		[ContextMenu("Fetch Directories")]
//		public void FetchDirectories() => FileSync.FetchDirectories(testDirectories).Forget();


		[ContextMenu("Test FileMap Sync")]
		public void TestFileMapSync() => FileMapSync().Forget();


		public async UniTask FileMapSync()
		{
			var fileMap = await FileSync.GetFileMap(testDirectories,
			                                        testFiles);
			
			await FileSync.SynchronizeFileMap(fileMap);
		}

	}

}
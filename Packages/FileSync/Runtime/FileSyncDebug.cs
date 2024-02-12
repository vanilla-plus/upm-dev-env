using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.FileSync
{

	public class FileSyncDebug : MonoBehaviour
	{

//		public long LocalFileMapTotalSize  = 0;
//		public long RemoteFileMapTotalSize = 0;
		public long FileMapSizeDiff        = 0;

		public long  CurrentDownloadBytesDownloaded = 0;
		public float CurrentDownloadPercentComplete = 0.0f;

		public FileSync.RemoteS3Object[] FileMap;

		[ContextMenu("Get FileMap")]
		public void GetCopyOfFileMap() => FileMap = FileSync.RemoteFileMap;


//		void Update()
//		{
////			LocalFileMapTotalSize          = FileSync.LocalFileMapTotalSize;
////			RemoteFileMapTotalSize         = FileSync.RemoteFileMapTotalSize;
//			FileMapSizeDiff                = FileSync.FileMapSizeDiff;
//			CurrentDownloadBytesDownloaded = FileSync.CurrentDownloadBytesDownloaded;
//			CurrentDownloadPercentComplete = FileSync.CurrentDownloadPercentComplete;
//
//		}

	}

}
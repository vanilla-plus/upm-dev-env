using System;
using System.IO;
using System.Linq;
using System.Threading;

using Amazon.S3.Model;
using Amazon.S3.Transfer;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.StringFormatting;

using static UnityEngine.Debug;

namespace Vanilla.Catalogue
{

	public class AWSCatalogueItem : CatalogueItem
	{

		public Texture2D thumbnail = new(width: 1,
		                                 height: 1);

		public Sprite sprite;

		[Header("Paths")]
		private string nameLower;
		
		public string LocalPath;
		public string RemotePath;
		public string ThumbnailPath;

		public override UniTask Initialize()
		{
			nameLower     = _name.ToLower();
			
			RemotePath    = Path.DirectorySeparatorChar    + nameLower;
			LocalPath     = Application.persistentDataPath + Path.DirectorySeparatorChar + "cache"                     + RemotePath;
			ThumbnailPath = Path.DirectorySeparatorChar    + "thumbnails"                + Path.DirectorySeparatorChar + nameLower + ".jpg";
			
			UpdateLocalByteSize();

			return default;
		}


		// --------------


//
	private void LoadThumbnail()
	{
		if (!File.Exists(path: ThumbnailPath))
		{
			LogError(message: $"Local thumbnail file not found for [{_name}]. It was expected at the following path:\n{ThumbnailPath}");

			return;
		}

		var thumbnailBytes = File.ReadAllBytes(path: ThumbnailPath);

		thumbnail.LoadImage(data: thumbnailBytes);

		sprite = Sprite.Create(texture: thumbnail,
		                       rect: new Rect(x: 0,
		                                      y: 0,
		                                      width: thumbnail.width,
		                                      height: thumbnail.height),
		                       pivot: new Vector2(x: 0.5f,
		                                          y: 0.5f),
		                       pixelsPerUnit: 100F,
		                       extrude: 0,
		                       meshType: SpriteMeshType.FullRect);
	}


		public void UpdateLocalByteSize()
		{
			localByteSize = Directory.Exists(path: LocalPath) ? (ulong)GetSizeOfDirectory(input: new DirectoryInfo(path: LocalPath)) : 0;

			localByteSizeString = localByteSize.AsDataSize();

			Log(message: $"Local byte size updated for [{_name}] - [{localByteSizeString}]");

			onLocalByteSizeUpdate?.Invoke(obj: localByteSizeString);
		}


		public long GetSizeOfDirectory(DirectoryInfo input) => input.GetFiles().Sum(selector: file => file.Length) + input.GetDirectories().Sum(selector: GetSizeOfDirectory);

		public string GetDownloadRequirement => (remoteByteSize - localByteSize).AsDataSize();

		public bool ContentFullyDownloaded => localByteSize != 0 && localByteSize == remoteByteSize;


		public async UniTask UpdateRemoteByteSize()
		{
			remoteByteSize = 0;

			//		var result = await S3.ListObjectsV2Async(key: EpisodeNameLower);

			var result = await AWSApp.client.ListObjectsV2Async(new ListObjectsV2Request
			                                                    {
				                                                    BucketName = AWSApp.c_S3BucketName,
				                                                    Prefix     = _name
			                                                    });

			var remoteObjectCount = result.S3Objects.Count;

			Log(message: $"Found [{remoteObjectCount}] remote objects for [{_name}]");

			if (remoteObjectCount == 0) return;

			var output = result.S3Objects.Sum(selector: o => o.Size);

			remoteByteSize = (ulong)output;

			remoteByteSizeString = remoteByteSize.AsDataSize();

			Log(message: $"Remote byte size updated for [{_name}] - [{remoteByteSizeString}]");

			onRemoteByteSizeUpdate?.Invoke(obj: remoteByteSizeString);
		}


		// --------------------------------------------------------------------------------------------------- Downloading //

		[Header("Download State")]
		[SerializeField]
		private bool _downloading = false;
		public bool Downloading => _downloading;

		private CancellationTokenSource _cancellationTokenSource;

		[SerializeField]
		private bool downloadCompleted = false;
		[SerializeField]
		private bool downloadCancelled = false;
		[SerializeField]
		private bool downloadFaulted = false;

		public int    downloadTotalNumberOfFiles               = -1;
		public int    downloadNumberOfFilesDownloaded          = -1;
		public long   downloadTotalBytes                       = -1;
		public long   downloadTransferredBytes                 = -1;
		public string downloadCurrentFile                      = string.Empty;
		public long   downloadTransferredBytesForCurrentFile   = -1;
		public long   downloadTotalNumberOfBytesForCurrentFile = -1;

		public ulong  remoteByteSize;
		public string remoteByteSizeString;

		public ulong  localByteSize;
		public string localByteSizeString;

		public Action<string> onRemoteByteSizeUpdate;
		public Action<string> onLocalByteSizeUpdate;

		public Action<string> onDownloadRequirementUpdate;

		public Action               onDownloadBegun;
		public Action<ulong, float> onDownloadPacket;
		public Action               onDownloadComplete;

		public Action<bool> onContentAvailabilityChange;

		public void UpdateDownloadRequirementText() => onDownloadRequirementUpdate?.Invoke(obj: GetDownloadRequirement);


		public async UniTask Download()
		{
			if (_downloading)
			{
				LogWarning(message: $"[{_name}] is already downloading.");

				return;
			}

			_downloading = true;

			downloadCompleted = downloadCancelled = downloadFaulted = false;

			Log($"[{_name}] content download begun.");

			onDownloadBegun?.Invoke();

			var request = new TransferUtilityDownloadDirectoryRequest
			              {
				              LocalDirectory = LocalPath,
				              BucketName     = AWSApp.c_S3BucketName,
				              S3Directory    = RemotePath
			              };

			// Reset all the download metrics

			downloadTotalNumberOfFiles               = -1;
			downloadNumberOfFilesDownloaded          = -1;
			downloadTotalBytes                       = -1;
			downloadTransferredBytes                 = -1;
			downloadTransferredBytesForCurrentFile   = -1;
			downloadTotalNumberOfBytesForCurrentFile = -1;

			request.DownloadedDirectoryProgressEvent += OnDownloadDirectoryProgress;

			_cancellationTokenSource = new CancellationTokenSource();

			var t = AWSApp.transferUtility.DownloadDirectoryAsync(request: request,
			                                                      cancellationToken: _cancellationTokenSource.Token);

			// The download takes a few frames to start.
			// We can wait for the first OnDownloadDirectoryProgress to come through until then.

			while (!(downloadCancelled || downloadFaulted) &&
			       downloadTransferredBytes == -1) await UniTask.Yield();

			while (!(downloadCompleted || downloadCancelled || downloadFaulted))
			{
				downloadCancelled = t.IsCanceled;
				downloadCompleted = t.IsCompleted;
				downloadFaulted   = t.IsFaulted;

				onDownloadPacket?.Invoke(arg1: (ulong)downloadTransferredBytes,
				                         arg2: (float)downloadTransferredBytes / downloadTotalBytes);

				await UniTask.Yield();
			}

			request.DownloadedDirectoryProgressEvent -= OnDownloadDirectoryProgress;

			_downloading = false;

			var downloadSuccessful = downloadTransferredBytes == downloadTotalBytes;

			if (downloadSuccessful)
			{
				Log($"[{_name}] content download complete.");

				onDownloadComplete?.Invoke();
			}

			onContentAvailabilityChange?.Invoke(downloadSuccessful);
		}


		private void OnDownloadDirectoryProgress(object s,
		                                         DownloadDirectoryProgressArgs a)
		{
			downloadTotalNumberOfFiles               = a.TotalNumberOfFiles;
			downloadNumberOfFilesDownloaded          = a.NumberOfFilesDownloaded;
			downloadTotalBytes                       = a.TotalBytes;
			downloadTransferredBytes                 = a.TransferredBytes;
			downloadCurrentFile                      = a.CurrentFile;
			downloadTransferredBytesForCurrentFile   = a.TransferredBytesForCurrentFile;
			downloadTotalNumberOfBytesForCurrentFile = a.TotalNumberOfBytesForCurrentFile;
		}


		public void CancelDownload()
		{
			if (!_downloading) return;

			_cancellationTokenSource.Cancel();

			_downloading = false;

			_cancellationTokenSource = null;
		}


		public async UniTask DeleteContent()
		{
			if (!Directory.Exists(path: LocalPath)) return;

			Directory.Delete(path: LocalPath,
			                 recursive: true);

			UpdateLocalByteSize();

			UpdateDownloadRequirementText();

			// If we don't let the method take a breather here, we seem to get performance spikes on one of the following frames.
			// If we remove this delay and turn the method back into a standard void, the next frames deltaTime is 0.333f (the projects maximum).
			// This may only be possible due to Editor overhead or a Windows-environment specific issue, but it can't hurt to give the delete
			// operation some breathing room anyway.

			await UniTask.Delay(250);

			onContentAvailabilityChange?.Invoke(obj: false);

		}



	}

}
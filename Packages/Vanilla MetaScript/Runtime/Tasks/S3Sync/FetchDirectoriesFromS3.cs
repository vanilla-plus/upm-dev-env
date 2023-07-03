//using System;
//using System.Linq;
//using System.Threading;
//
//using Cysharp.Threading.Tasks;
//
//using S3;
//
//using UnityEngine;
//
//using Vanilla.MetaScript;
//
//[Serializable]
//public class FetchDirectoriesFromS3 : MetaTask
//{
//	
//	public FetchableDirectory[] directories = Array.Empty<FetchableDirectory>();
//
//	public CanvasRenderer pleaseWaitUI;
//
////	protected override bool CanDescribe() => directories != null && directories.Length > 0 && directories.All(d => !string.IsNullOrEmpty(d.directory));
//	protected override bool CanDescribe() => directories is
//	                                         {
//		                                         Length: > 0
//	                                         };
//
//
//	protected override string DescribeTask() => directories.Aggregate(seed: "Download",
//	                                                                  func: (current,
//	                                                                         d) => current + $" {d.directory}{(d.includeSubdirectories ? " recursively" : string.Empty)}");
//
//
//	protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource)
//	{
//		var op = FileSync.FetchDirectories(directories);
//
//		var alpha = 1.0f;
//
//		float frequency = 2.0f; // Adjust the frequency of the sine wave (higher value means faster oscillation)
//
//		while (op.Status == UniTaskStatus.Pending)
//		{
//			// Make alpha fade back and forth between 1.0 and 0.0 as a smooth sin wave
//			alpha = (Mathf.Sin(Time.time * frequency) + 1.0f) / 2.0f;
//
//			pleaseWaitUI.SetAlpha(alpha);
//
//			await UniTask.Yield();
//		}
//
//		pleaseWaitUI.SetAlpha(1.0f);
//	}
//
//}
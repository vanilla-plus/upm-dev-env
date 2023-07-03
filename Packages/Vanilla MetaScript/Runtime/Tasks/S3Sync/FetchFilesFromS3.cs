//using System;
//using System.Linq;
//using System.Threading;
//
//using Cysharp.Threading.Tasks;
//
//using S3;
//
//using UnityEngine;
//using UnityEngine.UI;
//
//using Vanilla.MetaScript;
//
//[Serializable]
//public class FetchFilesFromS3 : MetaTask
//{
//	
//	public string[] files = Array.Empty<string>();
//
//	public CanvasRenderer pleaseWaitUI;
//	
//	protected override bool CanDescribe() => files is
//	                                         {
//		                                         Length: > 0
//	                                         };
//
//
//	protected override string DescribeTask() => files.Aggregate(seed: "Download",
//	                                                            func: (current,
//	                                                                   d) => current + $" {d}");
//
//
//	protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource)
//	{
//		var op = FileSync.FetchRelativeFiles(files);
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
//
//}
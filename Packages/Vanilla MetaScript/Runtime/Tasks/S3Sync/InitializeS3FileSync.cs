//using System;
//using System.Threading;
//
//using Cysharp.Threading.Tasks;
//
//using S3;
//
//using Vanilla.MetaScript;
//
//[Serializable]
//public class InitializeS3FileSync : MetaTask
//{
//
//	public string region = "ap-southeast-2";
//	public string bucket = "meta-sxsw";
//
//	protected override bool CanDescribe() => true;
//
//
//	protected override string DescribeTask() => "Initialize S3 FileSync";
//
//
//	protected override UniTask _Run(CancellationTokenSource cancellationTokenSource)
//	{
//		FileSync.Initialize(region,
//		                    bucket);
//
//		return default;
//	}
//
//}
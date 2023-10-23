#if vanilla_metascript
using System;

using Cysharp.Threading.Tasks;

using Vanilla.MetaScript;

namespace Vanilla.FileSync
{

	[Serializable]
	public class InitializeFileSync : MetaTask
	{

		public string remoteRoot = "https://{bucket}.s3.{region}.amazonaws.com/";

		protected override bool CanAutoName() => true;


		protected override string CreateAutoName() => "Initialize S3 FileSync";


		protected override UniTask<ExecutionTrace> _Run(ExecutionTrace trace)
		{
			FileSync.Initialize(remoteRoot);

			return UniTask.FromResult(trace);
		}

	}

}
#endif
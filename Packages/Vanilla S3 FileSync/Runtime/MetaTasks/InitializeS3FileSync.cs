using System;

using Cysharp.Threading.Tasks;

using Vanilla.MetaScript;

namespace Vanilla.S3
{

	[Serializable]
	public class InitializeS3FileSync : MetaTask
	{

		public string region = "ap-southeast-2";
		public string bucket = string.Empty;

		protected override bool CanAutoName() => true;


		protected override string CreateAutoName() => "Initialize S3 FileSync";


		protected override UniTask<Tracer> _Run(Tracer tracer)
		{
			FileSync.Initialize(region,
			                    bucket);

			return UniTask.FromResult(tracer);
		}

	}

}
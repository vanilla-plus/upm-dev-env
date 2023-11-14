#if vanilla_metascript
using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript;

namespace Vanilla.FileSync
{

	[Serializable]
	public class Init_File_Sync : MetaTask
	{

		public string remoteRoot = "https://{bucket}.s3.{region}.amazonaws.com/";

		public string localRoot = "fs";

		[Range(min: 0,max: 4)]
		public int pathSegmentsToSkip = 0;

		protected override bool CanAutoName() => true;


		protected override string CreateAutoName() => "Initialize S3 FileSync";


		protected override UniTask<Scope> _Run(Scope scope)
		{
			FileSync.Initialize(remoteRoot: remoteRoot,
			                    localRoot: localRoot,
			                    rootPathsToSkip: pathSegmentsToSkip);

			return UniTask.FromResult(scope);
		}

	}

}
#endif
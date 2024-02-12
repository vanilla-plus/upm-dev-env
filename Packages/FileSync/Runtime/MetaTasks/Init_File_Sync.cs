#if vanilla_metascript
using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.DataAssets;
using Vanilla.DataAssets.Sources;
using Vanilla.MetaScript;
using Vanilla.TypeMenu;

namespace Vanilla.FileSync
{

	[Serializable]
	public class Init_File_Sync : MetaTask
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		public String_Source remoteRoot = new Direct_String_Source("https://bucket.s3.region.amazonaws.com/");
		
		[SerializeReference]
		[TypeMenu("yellow")]
		public String_Source localRoot = new Direct_String_Source("fs");

		[Range(min: 0,max: 4)]
		public int pathSegmentsToSkip = 0;

		protected override bool CanAutoName() => remoteRoot != null && localRoot != null;

		protected override string CreateAutoName() => "Initialize FileSync";


		protected override UniTask<Scope> _Run(Scope scope)
		{
			FileSync.Initialize(remoteRoot: remoteRoot.Value,
			                    localRoot: localRoot.Value,
			                    rootPathsToSkip: pathSegmentsToSkip);

			return UniTask.FromResult(scope);
		}

	}

}
#endif
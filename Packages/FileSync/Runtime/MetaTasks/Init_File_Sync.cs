#if vanilla_metascript
using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;
using Vanilla.MetaScript;
using Vanilla.MetaScript.DataSources.Strings;

namespace Vanilla.FileSync
{

	[Serializable]
	public class Init_File_Sync : MetaTask
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		public StringSource remoteRoot = new StringSource_Direct
		                                 {
			                                 Value = "https: //bucket.s3.region.amazonaws.com/"
		                                 };

		[SerializeReference]
		[TypeMenu("yellow")]
		public StringSource localRoot = new StringSource_Concat_Path
		                                {
			                                Elements = new StringSource[]
			                                           {
				                                           new StringSource_Application_PersistentDataPath(),
				                                           new StringSource_Direct
				                                           {
					                                           Value = "fs"
				                                           }
			                                           }
		                                };

		[Tooltip("When listing specific folders or files, how many prefix directories should be ignored? For example, if set to 1, you can simply request the folder 'images' instead of 'myProject/images'")]
		[Range(min: 0,max: 8)]
		public int pathSegmentsToSkip = 0;

		protected override bool Validate => remoteRoot != null && localRoot != null;

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
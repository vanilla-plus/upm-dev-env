using System;
using System.Linq;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript;
using Vanilla.SmartValues;

namespace Vanilla.S3
{

	[Serializable]
	public class FetchS3Directories : MetaTask
	{
	
		[SerializeField]
		public FetchableDirectory[] directories = Array.Empty<FetchableDirectory>();

		[SerializeField]
		public SmartSinWave ProgressSinWave = new SmartSinWave("S3 Directory Fetch Progress",
		                                                       0.0f);
		protected override bool CanAutoName() => directories is
		                                         {
			                                         Length: > 0
		                                         };

		protected override string CreateAutoName() => directories.Length == 1 && directories[0].includeSubdirectories ?
			                                              "Download everything" :
			                                              "Download specific directories";


		protected override async UniTask<Tracer> _Run(Tracer tracer)
		{
			var op = FileSync.FetchDirectories(directories);

			while (op.Status == UniTaskStatus.Pending)
			{
				if (tracer.Cancelled(this)) return tracer;

				ProgressSinWave.Update(Time.deltaTime);

				await UniTask.Yield();
			}
		
			return tracer;
		}

	}

}
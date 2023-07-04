using System;
using System.Linq;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript;
using Vanilla.SmartValues;

namespace Vanilla.S3
{

	[Serializable]
	public class FetchS3Files : MetaTask
	{

		[SerializeField]
		public string[] files = Array.Empty<string>();

		[SerializeField]
		public SmartSinWave ProgressSinWave = new SmartSinWave("S3 File Fetch Progress",
		                                                       0.0f);


		protected override bool CanAutoName() => files is
		                                         {
			                                         Length: > 0
		                                         };
		
		protected override string CreateAutoName() => files.Length == 1 ?
			                                              $"Download [{files[0]}]" :
			                                              "Download specific files";

		protected override async UniTask<Tracer> _Run(Tracer tracer)
		{
			var op = FileSync.FetchRelativeFiles(files);

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
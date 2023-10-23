#if vanilla_metascript
using System;
using System.Linq;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

using Vanilla.MetaScript;

namespace Vanilla.FileSync
{

	[Serializable]
	public class PerformFileSync : MetaTask
	{

		[SerializeField]
		[Range(1,
		       8)]
		public int numberOfSimultaneousDownloads = 1;
		
		[SerializeField]
		public FetchableDirectory[] directories = Array.Empty<FetchableDirectory>();

		[SerializeField]
		public string[] files = Array.Empty<string>();
		
		[SerializeField]
		[Range(0.1f, 1.0f)]
		public float sinSpeed = 0.333f;
		
		[SerializeField]
		[Range(0.01f, 1.0f)]
		public float postLoadSmoothTime = 0.333f;



		[SerializeField]
		public UnityEvent<float> OnProgressSinWave = new UnityEvent<float>();

		protected override bool CanAutoName() => directories is
		                                         {
			                                         Length: > 0
		                                         };

		protected override string CreateAutoName() => directories.Length == 1 && directories[0].includeSubdirectories ?
			                                              "Download everything" :
			                                              "Download specific directories";


		protected override async UniTask<ExecutionTrace> _Run(ExecutionTrace trace)
		{
			var t = 0.0f;

			var fileMap = await FileSync.GetFileMap(directories,
			                                        files);

			var op = FileSync.SynchronizeFileMap(fileMap, numberOfSimultaneousDownloads);
			
			// Run a sin-wave while we download

			while (op.Status == UniTaskStatus.Pending)
			{
				if (trace.Cancelled) return trace;
//				if (trace.HasBeenCancelled(this)) return trace;

				t = Mathf.Repeat(t: t + Time.deltaTime * sinSpeed,
				                 1.0f);

				var sin = 0.5f * (float) Math.Sin(2 * Math.PI * t) + 0.5f;
				
				OnProgressSinWave.Invoke(sin);

				await UniTask.Yield();
			}

			// Gently slingshot .Time back to 0.0f
			
			var tVelocity = 0.0f;
			
			while (t > 0.001f)
			{
				if (trace.Cancelled) return trace;
//				if (trace.HasBeenCancelled(this)) return trace;

				t = Mathf.SmoothDamp(t,
				                     0.0f,
				                     ref tVelocity,
				                     postLoadSmoothTime);
				
				OnProgressSinWave.Invoke(t);
				
				await UniTask.Yield();
			}
		
			return trace;
		}

	}

}
#endif
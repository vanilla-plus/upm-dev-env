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
	public class Sync_File_Map : MetaTask
	{

		[SerializeField]
		[Range(min: 1,
		       max: 8)]
		public int numberOfSimultaneousDownloads = 1;
		
		[SerializeField]
		[Range(min: 0.1f, max: 1.0f)]
		public float sinSpeed = 0.333f;
		
		[SerializeField]
		[Range(min: 0.01f, max: 1.0f)]
		public float postLoadSmoothTime = 0.333f;



		[SerializeField]
		public UnityEvent<float> OnProgressSinWave = new UnityEvent<float>();

		protected override bool CanAutoName() => true;

		protected override string CreateAutoName() => "Download missing FileMap contents";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			var t = 0.0f;

			var op = FileSync.SynchronizeFileMap(fileMap: FileSync.RemoteFileMap,
			                                     numberOfSimultaneousDownloads: numberOfSimultaneousDownloads);
			
			// Run a sin-wave while we download

			while (op.Status == UniTaskStatus.Pending)
			{
				if (scope.Cancelled) return scope;

				t = Mathf.Repeat(t: t + Time.deltaTime * sinSpeed,
				                 length: 1.0f);

				var sin = 0.5f * (float) Math.Sin(2 * Math.PI * t) + 0.5f;
				
				OnProgressSinWave.Invoke(sin);

				await UniTask.Yield();
			}
			
			// Gently slingshot .Time back to 0.0f
			
			var tVelocity = 0.0f;
			
			while (t > 0.001f)
			{
				if (scope.Cancelled) return scope;

				t = Mathf.SmoothDamp(current: t,
				                     target: 0.0f,
				                     currentVelocity: ref tVelocity,
				                     smoothTime: postLoadSmoothTime);
				
				OnProgressSinWave.Invoke(t);
				
				await UniTask.Yield();
			}
			
			return scope;
		}

	}

}
#endif
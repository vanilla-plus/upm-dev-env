#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class MetaTaskInstance : MonoBehaviour, IRunnable
	{
		
		[SerializeReference]
		[TypeMenu]
		[Only(typeof(MetaTask))]
		private MetaTask task;
		public MetaTask Task => task;

		void OnValidate()
		{
			#if UNITY_EDITOR
			task?.OnValidate();
			#endif
		}
		
		public async UniTask<ExecutionTrace> Run(ExecutionTrace trace) => await task.Run(trace);

	}

}
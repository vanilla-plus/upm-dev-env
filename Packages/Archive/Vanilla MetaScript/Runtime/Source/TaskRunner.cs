using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{

	// Wait wait... I don't think you would try and DataAsset-ify Tasks or Task Runners ever.
	// You would do that to a TaskSet, but not individual tasks because they can and often will operate on scene-only GameObjects and components.
	
	[Serializable]
	public class TaskRunner
	{

		[SerializeField]
		[HideInInspector]
		private string description;

		public void UpdateDescription() => description = task?.GetDescription();

		[SerializeField]
		public bool asynchronous = false;

		[SerializeReference] 
		[TypeMenu]
		public TaskBase task;

		public virtual void OnValidate()
		{
			task?.OnValidate();

			UpdateDescription();
		}


		public async UniTask Run()
		{
//			task.cancelled = false;
//			task.running = true;
			
			#if DEBUG_METASCRIPT
			Debug.Log($"Task begun - [{task.GetDescription()}]");
			#endif
			
			if (asynchronous)
			{
				task.Run();
			}
			else
			{
				await task.Run();
			}

			#if DEBUG_METASCRIPT
			Debug.Log($"Task completed - [{task.GetDescription()}]");
			#endif
			
//			task.running   = false;
//			task.cancelled = false;
		}

	}

}
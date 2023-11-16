using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class MetaTaskInstance : MonoBehaviour
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

			if (task is
			    {
				    scopeSource: null
			    })
			{
				var newScopeSource = new Named_Scope_Source
				                     {
					                     name = "root"
				                     };

				task.scopeSource = newScopeSource;
			}
			#endif
		}

		[ContextMenu("Start Task")]
		public void StartTask() => task.Run(null).Forget();

	}

}
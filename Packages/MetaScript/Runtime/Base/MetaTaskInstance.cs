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
		[TypeMenu("blue")]
		private MetaTask task;
		public MetaTask Task => task;

		void OnValidate()
		{
			#if UNITY_EDITOR
			task?.OnValidate();
			#endif
		}


		[ContextMenu(itemName: "Start Task")]
		private void EditorStart() => StartTask(null);
		
//		public void StartTask(Scope scope) => task.Run(scope: scope).Forget();
		public void StartTask(Scope scope) => task.Run(scope)
		                                          .ContinueWith(s =>
		                                                        {
//			                                                        Debug.LogError("The eagle has landed.");
			                                                        
//			                                                        Debug.Log(s.Name);

			                                                        scope.Cancel();

			                                                        scope.Dispose();
			                                                        
//			                                                        s.Cancel();

//			                                                        s.Dispose();
		                                                        });

	}

}
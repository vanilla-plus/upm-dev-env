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


		[ContextMenu(itemName: "Test Run")]
		private void EditorStart()
		{
			var newScope = new Scope("editor");

			task?.Run(newScope).Forget();
		}


		public void Run(string scopeName)
		{
			var newScope = new Scope(scopeName);
			
			task?.Run(newScope).Forget();
		}


		public async UniTask<Scope> RunAsync(string scopeName)
		{
			var newScope = new Scope(scopeName);

			if (task == null) return newScope;
			
			return await task.Run(newScope);
		}


		//		public void StartTask(Scope scope) => task.Run(scope: scope).Forget();

//		public void StartTask(Scope scope) => task.Run(scope);
//
//		public void StartTask(Scope scope) => task.Run(scope)
//		                                          .ContinueWith(s =>
//		                                                        {
//			                                                        Debug.LogError("The eagle has landed.");
//			                                                        
//			                                                        Debug.Log(s.Name);
//
//			                                                        scope.Cancel();
//
//			                                                        scope.Dispose();
//			                                                        
////			                                                        s.Cancel();
//
////			                                                        s.Dispose();
//		                                                        });

	}

}
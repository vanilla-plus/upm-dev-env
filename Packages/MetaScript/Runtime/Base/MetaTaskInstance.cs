using System;
using System.Collections.Generic;

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

		[NonSerialized]
		public List<Scope> scopes;
		
		void OnValidate()
		{
			#if UNITY_EDITOR
			task?.OnValidate();
			#endif
		}


		public async UniTask<Scope> Run(Scope scope)
		{
			if (task != null)
			{
				await task.Run(scope);
			}
			
			return scope;
		}


		[ContextMenu("Start")]
		public void StartTask() => task.Run(NewRootScope).ContinueWith(s => s.Dispose()); // This is when 'root' scopes are disposed


		[ContextMenu("Start And Cache Scope")]
		public void StartTaskAndCacheScope()
		{
			var scope = NewRootScope;

			scopes ??= new List<Scope>();
			
			scopes.Add(scope);
			
			task.Run(scope).ContinueWith(s => s.Dispose()); // This is when 'root' scopes are disposed
		}


		private Scope NewRootScope => new Scope(null,
		                                        $"root [{gameObject.name}]",
		                                        "MetaTaskInstance");
		
		
		[ContextMenu("Cancel All")]
		public void Cancel_All()
		{
			for (var i = scopes.Count - 1;
			     i >= 0;
			     i--)
			{
				scopes[i]?.Cancel();
			}

			scopes.Clear();
		}


		[ContextMenu("Cancel All Recursive")]
		public void Cancel_All_Recursive()
		{
			for (var i = scopes.Count - 1;
			     i >= 0;
			     i--)
			{
				scopes[i]?.Cancel_Recursive();
			}

			scopes.Clear();
		}
		
		public void Cancel_All_Upwards(int layersOfCancellation = 1)
		{
			for (var i = scopes.Count - 1;
			     i >= 0;
			     i--)
			{
				scopes[i]?.Cancel_Upwards(layersOfCancellation);
			}

			scopes.Clear();
		}
		
		public void Cancel_At_Index(int index = 0)
		{
			if (index < 0 || index >= scopes.Count)
			{
				#if debug
				Debug.LogWarning($"No Scope at index [{index}]");
				#endif
				
				return;
			}
			
			scopes[index]?.Cancel();
			
			scopes.RemoveAt(index);
		}


		public void Cancel_At_Index_Recursive(int index = 0)
		{
			if (index < 0 || index >= scopes.Count)
			{
				#if debug
				Debug.LogWarning($"No Scope at index [{index}]");
				#endif
				
				return;
			}
			
			scopes[index]?.Cancel_Recursive();
			
			scopes.RemoveAt(index);
		}


		public void Cancel_At_Index_Upwards(int index = 0, int layersOfCancellation = 1)
		{
			if (index < 0 || index >= scopes.Count)
			{
				#if debug
				Debug.LogWarning($"No Scope at index [{index}]");
				#endif
				
				return;
			}
			
			scopes[index]?.Cancel_Upwards(layersOfCancellation);
			
			scopes.RemoveAt(index);
		}

	}

}
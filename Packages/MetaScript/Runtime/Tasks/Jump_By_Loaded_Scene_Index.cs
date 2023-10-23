using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using UnityEngine.SceneManagement;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	public class Jump_By_Loaded_Scene_Index : MetaTask
	{

		public int TargetSceneIndex;

		protected override bool CanAutoName() => true;


		protected override string CreateAutoName() => $"Jump to loaded scene [{TargetSceneIndex}]";
        
		protected override async UniTask<ExecutionTrace> _Run(ExecutionTrace trace)
		{
			if (TargetSceneIndex < 0 ||
			    TargetSceneIndex > SceneManager.loadedSceneCount)
			{
				Debug.LogError($"Invalid Loaded Scene Index [{TargetSceneIndex}]");

//				trace.Continue = false;
				trace.scope.Cancel();

				return trace;
			}
			
			var rootObjects = SceneManager.GetSceneAt(TargetSceneIndex).GetRootGameObjects();

			if (rootObjects == null)
			{
//				trace.Continue = false;
				trace.scope.Cancel();

				return trace;
			}

			MetaTaskInstance instance = null;

			Debug.Log($"Contains [{rootObjects.Length}] root GameObjects");
            
			foreach (var o in rootObjects)
			{
				Debug.LogError($"Checking [{o.gameObject.name}] for a MetaScriptInstance...");
                
				instance = o.GetComponent<MetaTaskInstance>();

				if (instance != null) break;
			}

			if (instance == null)
			{
				Debug.LogWarning($"Couldn't find a MetaScriptInstance attached to any root GameObject in the scene loaded at index [{TargetSceneIndex}].");
                
//				trace.Continue = false;
				trace.scope.Cancel();

				return trace;
			}

			await instance.Run(trace);
            
			return trace;
		}

	}
}
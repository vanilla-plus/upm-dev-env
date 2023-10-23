using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using UnityEngine.SceneManagement;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class Jump_By_Scene_Name : MetaTask
    {

        public string TargetSceneName;

        protected override bool CanAutoName() => !string.IsNullOrEmpty(TargetSceneName);


        protected override string CreateAutoName() => $"Jump to [{TargetSceneName}] scene";
        
        protected override async UniTask<ExecutionTrace> _Run(ExecutionTrace trace)
        {
            GameObject[] rootObjects = null;
            
            for (var i = 0;
                 i < SceneManager.loadedSceneCount;
                 i++)
            {
                var s = SceneManager.GetSceneAt(i);

                if (s.name == TargetSceneName)
                {
                    rootObjects = s.GetRootGameObjects();
                }
            }

            if (rootObjects == null)
            {
//                trace.Continue = false;
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
                Debug.LogWarning($"Couldn't find a MetaScriptInstance attached to any root GameObject in the [{TargetSceneName}] scene.");
                
                trace.scope.Cancel();
                
                return trace;
            }

            await instance.Run(trace);
            
            return trace;
        }

    }
}

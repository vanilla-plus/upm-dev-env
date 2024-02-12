using System;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vanilla.MetaScript.Flow
{
    
    [Serializable]
    public class Jump_By_Scene_Name : MetaTask
    {

        public string TargetSceneName;

        protected override bool CanAutoName() => !string.IsNullOrEmpty(TargetSceneName);


        protected override string CreateAutoName() => $"Jump to scene [{TargetSceneName}]";
        
        protected override async UniTask<Scope> _Run(Scope scope)
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
//                scope.Cancel();

                return scope;
            }

            MetaTaskInstance instance = null;

            foreach (var o in rootObjects)
            {
                instance = o.GetComponent<MetaTaskInstance>();

                if (instance != null) break;
            }

            if (instance == null)
            {
                Debug.LogWarning($"Couldn't find a MetaScriptInstance attached to any root GameObject in the [{TargetSceneName}] scene.");
                
//                scope.Cancel();
                
                return scope;
            }

            if (scope.Cancelled) return scope;

            if (instance.Task != null) await instance.Task.Run(scope);

            
            return scope;
        }

    }
}

using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using UnityEngine.SceneManagement;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class JumpToSceneByName : MetaTask
    {

        public string TargetSceneName;

        protected override bool CanAutoName() => !string.IsNullOrEmpty(TargetSceneName);


        protected override string CreateAutoName() => $"Jump to [{TargetSceneName}] scene";
        
        protected override async UniTask<Tracer> _Run(Tracer tracer)
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
                tracer.Continue = false;
                
                return tracer;
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
                
                tracer.Continue = false;
                
                LogRunCancelled(tracer);

                return tracer;
            }

            await instance.HandleJump(tracer);
            
            return tracer;
        }

    }
}

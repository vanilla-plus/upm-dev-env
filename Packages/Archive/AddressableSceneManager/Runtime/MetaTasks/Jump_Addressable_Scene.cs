#if unity_addressables && vanilla_metascript
using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

using SceneManager = Vanilla.AddressableSceneManager.AddressableSceneManager;

namespace Vanilla.MetaScript.Addressables
{

    [Serializable]
    public class Jump_Addressable_Scene : MetaTask
    {

        [SerializeField]
        public AssetReference assRef;

        [SerializeField]
        public string TargetSceneName;

        [SerializeField]
        public LoadSceneMode loadMode = LoadSceneMode.Additive;

        [SerializeField]
        public bool activateOnLoad = true;


        public override void OnValidate()
        {
            #if UNITY_EDITOR
            if (assRef             != null &&
                assRef.editorAsset != null) TargetSceneName = assRef.editorAsset.name;

            base.OnValidate();
            #endif
        }


        protected override bool CanAutoName() => !string.IsNullOrEmpty(TargetSceneName);


        protected override string CreateAutoName() => $"Jump to [{TargetSceneName}] scene";


        protected override async UniTask<Scope> _Run(Scope scope)
        {
            // Load
            var loadOperation = SceneManager.TryLoadSceneInstance(assRef: assRef,
                                                                  loadMode: loadMode,
                                                                  activateOnLoad: activateOnLoad);

            while (loadOperation.Status == UniTaskStatus.Pending)
            {
                if (scope.Cancelled) return scope;
//                if (trace.HasBeenCancelled(this)) return trace;

                await UniTask.Yield();
            }

            Debug.Log($"Load operation status [{loadOperation.Status}]");

            if (loadOperation.Status is UniTaskStatus.Faulted or UniTaskStatus.Canceled)
            {
//                scope.Cancel();
                
                return scope;
            }

            // Jump

            var sceneInstance = await loadOperation;

            MetaTaskInstance instance = null;

            foreach (var o in sceneInstance.Scene.GetRootGameObjects())
            {
                instance = o.GetComponent<MetaTaskInstance>();

                if (instance != null) break;
            }

            if (instance == null)
            {
                Debug.LogWarning($"Couldn't find a MetaScriptInstance attached to any root GameObject in the [{TargetSceneName}] scene.");

//                trace.Continue = false;
//                scope.Cancel();

//                LogRunCancelled(trace: trace);

                return scope;
            }

//            await instance.Run(scope);
            if (instance.Task != null) await instance.Task.Run(scope);

            // Unload

            var unloadOperation = SceneManager.TryUnloadSceneInstance(assRef).AsUniTask();

            while (unloadOperation.Status == UniTaskStatus.Pending)
            {
                if (scope.Cancelled) return scope;
//                if (trace.HasBeenCancelled(this)) return trace;

                await UniTask.Yield();
            }

            return scope;
        }

    }

}
#endif
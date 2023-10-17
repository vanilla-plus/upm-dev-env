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
    public class JumpAddressableScene : MetaTask
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


        protected override async UniTask<Tracer> _Run(Tracer tracer)
        {
            // Load
            var loadOperation = SceneManager.TryLoadSceneInstance(assRef: assRef,
                                                                  loadMode: loadMode,
                                                                  activateOnLoad: activateOnLoad);

            while (loadOperation.Status == UniTaskStatus.Pending)
            {
                if (tracer.HasBeenCancelled(this)) return tracer;

                await UniTask.Yield();
            }

            Debug.Log($"Load operation status [{loadOperation.Status}]");

            if (loadOperation.Status is UniTaskStatus.Faulted or UniTaskStatus.Canceled) return tracer;

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

                tracer.Continue = false;

                LogRunCancelled(tracer: tracer);

                return tracer;
            }

            await instance.Run(tracer: tracer);

            // Unload

            var unloadOperation = SceneManager.TryUnloadSceneInstance(assRef).AsUniTask();

            while (unloadOperation.Status == UniTaskStatus.Pending)
            {
                if (tracer.HasBeenCancelled(this)) return tracer;

                await UniTask.Yield();
            }

            return tracer;
        }

    }

}
#endif
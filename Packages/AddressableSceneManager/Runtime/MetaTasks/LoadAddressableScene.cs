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
    public class LoadAddressableScene : MetaTask
    {

        [SerializeField]
        public AssetReference assRef;

        [SerializeField]
        public LoadSceneMode loadMode = LoadSceneMode.Additive;

        [SerializeField]
        public bool activateOnLoad = true;
        
        public override void OnValidate()
        {
            #if UNITY_EDITOR
            if (assRef.editorAsset != null && 
                assRef.editorAsset.GetType().Name != "SceneAsset")
            {
                assRef.SetEditorAsset(null);
            }

            base.OnValidate();
            #endif
        }


        protected override bool CanAutoName() => assRef.editorAsset != null;


        protected override string CreateAutoName() => $"Load scene [{assRef.editorAsset.name}] ({(loadMode == LoadSceneMode.Single ? "Destructive" : "Additive")})";


        protected override async UniTask<Scope> _Run(Scope scope)
        {
            var operation = SceneManager.TryLoadSceneInstance(assRef,
                                                              loadMode,
                                                              activateOnLoad);

            while (operation.Status == UniTaskStatus.Pending)
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
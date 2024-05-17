using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript;
using Vanilla.MetaScript.DataSources.Strings;
using Vanilla.TypeMenu;

namespace Vanilla.Hambundler
{
    
    [Serializable]
    public class Unload_Bundle : MetaTask
    {

        [TypeMenu("red")]
        [SerializeReference] public StringSource BundleName;
//        public string bundleName;

        public Action<float> OnUnloadProgress;
        
        protected override bool Validate => BundleName != null && !string.IsNullOrWhiteSpace(BundleName.Value);


        protected override string CreateAutoName() => $"Unload bundle [{BundleName.Value}]";

        protected override async UniTask<Scope> _Run(Scope scope)
        {
            Hambundler.Bundles.TryGetValue(key: BundleName.Value,
                                value: out var bundle);

            if (bundle == null)
            {
                Debug.LogError($"No loaded bundle by the name [{BundleName.Value}]");
                
                return scope;
            }
            
            Hambundler.Bundles.Remove(BundleName.Value);

            #if debug
            Debug.Log($"AssetBundle unload begun - [{BundleName.Value}]");
            #endif
            
            var op = bundle.UnloadAsync(true);

            while (!op.isDone)
            {
                if (scope.Cancelled) return scope;
                
                OnUnloadProgress?.Invoke(op.progress);

                await UniTask.Yield();
            }
            
            #if debug
            Debug.Log($"AssetBundle unload successful - [{BundleName.Value}]");
            #endif

            return scope;
        }

    }
}

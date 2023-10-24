using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript;

namespace Vanilla.Hambundler
{
    
    [Serializable]
    public class Unload_Bundle : MetaTask
    {

        public string bundleName;

        public Action<float> OnUnloadProgress;
        
        protected override bool CanAutoName() => !string.IsNullOrWhiteSpace(bundleName);


        protected override string CreateAutoName() => $"Unload bundle [{bundleName}]";

        protected override async UniTask<Scope> _Run(Scope scope)
        {
            Hambundler.Bundles.TryGetValue(key: bundleName,
                                value: out var bundle);

            if (bundle == null)
            {
                Debug.LogError($"No loaded bundle by the name [{bundleName}]");
                
                return scope;
            }
            
            Hambundler.Bundles.Remove(bundleName);

            #if debug
            Debug.Log($"AssetBundle unload begun - [{bundleName}]");
            #endif
            
            var op = bundle.UnloadAsync(true);

            while (!op.isDone)
            {
                if (scope.Cancelled) return scope;
                
                OnUnloadProgress?.Invoke(op.progress);

                await UniTask.Yield();
            }
            
            #if debug
            Debug.Log($"AssetBundle unload successful - [{bundleName}]");
            #endif

            return scope;
        }

    }
}

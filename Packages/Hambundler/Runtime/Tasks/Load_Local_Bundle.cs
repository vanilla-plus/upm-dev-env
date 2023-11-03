#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript;

namespace Vanilla.Hambundler
{
    
    [Serializable]
    public class Load_Local_Bundle : MetaTask
    {

        public string        bundlePath;
        public string        bundleName;
        public Action<float> OnLoadProgress;
        
        protected override bool CanAutoName() => !string.IsNullOrWhiteSpace(bundlePath);


        protected override string CreateAutoName() => $"Load bundle [{bundleName}] from local file [{bundlePath}]";

        public override void OnValidate()
        {
            #if UNITY_EDITOR
            base.OnValidate();

            bundleName = Path.GetFileName(bundlePath);
            #endif
        }
        
        protected override async UniTask<Scope> _Run(Scope scope)
        {
            if (Hambundler.Bundles.ContainsKey(bundleName))
            {
                Debug.Log($"The bundle [{bundleName}] has already been loaded.");

                return scope;
            }
            
            #if debug
            Debug.Log($"AssetBundle local load begun - [{bundlePath}]");
            #endif
            
            var op = AssetBundle.LoadFromFileAsync(bundlePath);

            while (!op.isDone)
            {
                if (scope.Cancelled) return scope;
                
                OnLoadProgress?.Invoke(op.progress);

                await UniTask.Yield();
            }
            
            #if debug
            Debug.Log($"AssetBundle local load successful - [{bundlePath}]");
            #endif

            Hambundler.Bundles.Add(bundleName,
                                   op.assetBundle);

            return scope;
        }

    }
}

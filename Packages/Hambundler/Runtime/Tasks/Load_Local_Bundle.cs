using System;
using System.IO;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript;
using Vanilla.MetaScript.DataSources.Strings;
using Vanilla.TypeMenu;

namespace Vanilla.Hambundler
{
    
    [Serializable]
    public class Load_Local_Bundle : MetaTask
    {

        [TypeMenu("red")]
        [SerializeReference] public StringSource BundlePath;

        [TypeMenu("red")]
        [SerializeReference] public StringSource BundleName;

//        public string        bundlePath;
//        public string        bundleName;
        public Action<float> OnLoadProgress;
        
        protected override bool Validate => BundlePath != null && BundleName != null && !string.IsNullOrWhiteSpace(BundlePath.Value) && !string.IsNullOrWhiteSpace(BundleName.Value);


        protected override string CreateAutoName() => $"Load bundle [{BundleName}] from local file [{BundlePath}]";

        public override void OnValidate()
        {
            #if UNITY_EDITOR
            base.OnValidate();

            if (BundlePath != null &&
                BundleName != null)
            {
                BundleName.Value = Path.GetFileName(BundlePath.Value);
            }
            #endif
        }
        
        protected override async UniTask<Scope> _Run(Scope scope)
        {
            if (Hambundler.Bundles.ContainsKey(BundleName.Value))
            {
                #if debug
                Debug.Log($"The bundle [{BundleName.Value}] has already been loaded.");
                #endif

                return scope;
            }
            
            #if debug
            Debug.Log($"AssetBundle local load begun - [{BundlePath.Value}]");
            #endif
            
            var op = AssetBundle.LoadFromFileAsync(BundlePath.Value);

            while (!op.isDone)
            {
                if (scope.Cancelled) return scope;
                
                OnLoadProgress?.Invoke(op.progress);

                await UniTask.Yield();
            }
            
            #if debug
            Debug.Log($"AssetBundle local load successful - [{BundlePath.Value}]");
            #endif

            Hambundler.Bundles.Add(BundleName.Value,
                                   op.assetBundle);

            return scope;
        }

    }
}

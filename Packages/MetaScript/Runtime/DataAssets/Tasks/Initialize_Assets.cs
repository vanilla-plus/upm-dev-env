using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;

namespace Vanilla.MetaScript
{

    [Serializable]
    public class Initialize_Asset_Values : MetaTask
    {

        [SerializeReference] public BaseAsset[] Assets = Array.Empty<BaseAsset>();

        protected override bool Valid => Assets != null;

        protected override string CreateAutoName() => $"Reset [{Assets.Length}] assets to their default values";
        
        protected override UniTask<Scope> _Run(Scope scope)
        {
            

            foreach (var a in Assets) a.Reset();

            return UniTask.FromResult(scope);
        }

    }

}
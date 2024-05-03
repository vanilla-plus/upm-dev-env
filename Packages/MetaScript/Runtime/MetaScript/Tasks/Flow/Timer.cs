using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class Timer : MetaTask
    {

        [SerializeField] public AssetFloatSource timerAsset;

        protected override bool Valid => timerAsset != null && timerAsset.Asset != null && timerAsset.Asset.Source != null;

        protected override string CreateAutoName() => null;


        protected async override UniTask<Scope> _Run(Scope scope)
        {
            timerAsset.Value = timerAsset.Asset.DefaultValue;

            
            
            return scope;
        }

    }
}

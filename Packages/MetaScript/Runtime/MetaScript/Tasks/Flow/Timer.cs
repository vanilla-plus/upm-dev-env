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

        [SerializeField] public FloatSource_Asset TimerFloatSourceAsset;

        protected override bool Validate => TimerFloatSourceAsset != null && TimerFloatSourceAsset.Asset != null && TimerFloatSourceAsset.Asset.Source != null;

        protected override string CreateAutoName() => null;


        protected async override UniTask<Scope> _Run(Scope scope)
        {
            TimerFloatSourceAsset.Value = TimerFloatSourceAsset.Asset.DefaultValue;

            
            
            return scope;
        }

    }
}

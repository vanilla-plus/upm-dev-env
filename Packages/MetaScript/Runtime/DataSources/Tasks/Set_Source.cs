using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.MetaScript;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

    [Serializable]
    public abstract class Set_Source<PayloadType, Source, AssetSource> : MetaTask
        where PayloadType : struct
        where Source : IDataSource<PayloadType>
        where AssetSource : IAssetSource<PayloadType>, new()
    {

        [SerializeReference]
        [TypeMenu("red")]
        public Source source;

        [SerializeReference]
        [TypeMenu("red")]
        public Source target;

        protected override bool CanAutoName() => source != null && target != null;


        protected override string CreateAutoName() => $"Set [{(source is AssetSource s && s.Asset != null ? s.Asset.name : source.GetType().Name)}] to [{(target is AssetSource t && t.Asset != null ? t.Asset.name : target.GetType().Name)}]";


        protected override UniTask<Scope> _Run(Scope scope)
        {
            if (scope.Cancelled) return UniTask.FromResult(scope);

            if (source != null &&
                target != null)
            {
                source.Value = target.Value;
            }

            return UniTask.FromResult(scope);
        }

    }

}
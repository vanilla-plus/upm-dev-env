using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.MetaScript;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{
    
    [Serializable]
    public class Flip_Bool_Source : MetaTask
    {

        [SerializeReference]
        [TypeMenu("red")]
        public BoolSource source = new AssetBoolSource();

        protected override bool CanAutoName() => source != null;


        protected override string CreateAutoName() => $"Flip [{(source is AssetBoolSource boolSource ? boolSource.Asset.name : source.GetType().Name)}]";


        protected override UniTask<Scope> _Run(Scope scope)
        {
            if (scope.Cancelled) return UniTask.FromResult(scope);
            
            source?.Flip();
            
            return UniTask.FromResult(scope);
        }

    }
}

using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.Flow
{
    
    [Serializable]
    public class Close_Scope : MetaTask
    {

        [SerializeField]
        public bool thisScope = false;
        
        [SerializeField]
        public string targetScope;
        
        protected override bool CanAutoName() => !string.IsNullOrEmpty(value: targetScope);


        protected override string CreateAutoName() => $"Close {(thisScope ? "[this scope]" : $"the [{targetScope}] scope")}";


        protected override UniTask<Scope> _Run(Scope scope)
        {
            if (scope.Cancelled) return UniTask.FromResult(value: scope);

            if (thisScope)
            {
                scope.Cancel();
            }
            else
            {
                Scope.TryCancel(name: targetScope);
            }

            return UniTask.FromResult(value: scope);
        }

    }
}

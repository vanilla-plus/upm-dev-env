using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class Cancel : MetaTask
    {

        [SerializeField]
        public bool thisScope = false;
        
        [SerializeField]
        public string targetScope;
        
        protected override bool CanAutoName() => !string.IsNullOrEmpty(value: targetScope);


        protected override string CreateAutoName() => $"Cancel {(thisScope ? "[this scope]" : $"the [{targetScope}] scope")}";


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

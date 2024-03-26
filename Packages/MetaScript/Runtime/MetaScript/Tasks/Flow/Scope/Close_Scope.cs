using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.Flow
{
    
    [Serializable]
    public class Close_Scope : MetaTask
    {

        [SerializeField] public bool thisScope = false;
        
        [SerializeField] public string targetScope;
        
        protected override bool CanAutoName() => thisScope || !string.IsNullOrEmpty(value: targetScope);


        protected override string CreateAutoName() => $"Close {(thisScope ? "[this scope]" : $"the [{targetScope}] scope")}";


        protected override UniTask<Scope> _Run(Scope scope)
        {
            if (scope.Cancelled) return UniTask.FromResult(value: scope.parent);

//            else
//                Debug.LogWarning($"[{scope}] is cancelled? [{scope.Cancelled}] but who am I trying to cancel? [{targetScope}]");
            
            var nextScopeUp = scope.parent;
            
            if (thisScope)
            {
                scope.Cancel();
            }
            else
            {
                Scope.TryCancel(name: targetScope);
            }

//            return UniTask.FromResult(value: scope);
//            return UniTask.FromResult(value: parentScopeToReturnTo);

//            var nextScopeUp = scope.GetLastActiveScope();

//            Debug.Log($"NextScopeUp is [{nextScopeUp}]");
            
            return UniTask.FromResult(value: nextScopeUp);
//            return s.GetLastActiveScope();

        }

    }
}

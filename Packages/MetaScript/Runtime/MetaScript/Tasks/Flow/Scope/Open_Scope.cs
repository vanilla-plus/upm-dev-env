using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class Open_Scope : MetaTask
    {

        [SerializeReference]
        [TypeMenu("green")]
        public IScopeSource scopeSource;
        
        protected override bool CanAutoName() => scopeSource != null;
        
        protected override string CreateAutoName() => $"Open a new [{scopeSource.GetType().Name}] scope";


        protected override UniTask<Scope> _Run(Scope scope)
        {
            var s = scopeSource.CreateScope(scope);

            return UniTask.FromResult(s);
        }

    }
}

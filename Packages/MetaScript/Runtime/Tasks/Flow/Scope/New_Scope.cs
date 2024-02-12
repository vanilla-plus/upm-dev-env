using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class New_Scope : MetaTask
    {

        [SerializeReference]
        [TypeMenu("green")]
        public IScopeSource scopeSource;
        
        protected override bool CanAutoName() => false;


        protected override string CreateAutoName() => null;


        protected override UniTask<Scope> _Run(Scope scope)
        {
            var s = scopeSource.CreateScope(scope);

            return UniTask.FromResult(s);
        }

    }
}

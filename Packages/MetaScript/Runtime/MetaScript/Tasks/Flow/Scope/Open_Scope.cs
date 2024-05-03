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
        
        protected override bool Valid => scopeSource != null;


        protected override string CreateAutoName() => scopeSource switch
                                                      {
                                                          Named_Scope_Source s      => $"Open a new [{s}] scope",
                                                          Randomized_Scope_Source s => $"Open a randomised scope",
                                                          Indexed_Scope_Source s    => $"Open an indexed [{s.prefix}] scope",
                                                          _                         => "?"
                                                      };


        protected override UniTask<Scope> _Run(Scope scope)
        {
            var s = scopeSource.CreateScope(scope);

            return UniTask.FromResult(s);
        }

    }
}

using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class Jump : MetaTask
    {

        [SerializeField]
        public MetaTaskInstance target;
        
        protected override bool CanAutoName() => target && target.Task != null;

        protected override string CreateAutoName() => $"Jump to [{target.Task.Name}]";

        protected override async UniTask<Scope> _Run(Scope scope)
        {
            if (scope.Cancelled) return scope;
            
            if (target != null && target.Task != null) await target.Task.Run(scope);

            return scope;
        }

    }
}

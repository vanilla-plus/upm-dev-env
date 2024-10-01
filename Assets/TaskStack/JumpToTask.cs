using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace MagicalProject
{
    
    [Serializable]
    public class JumpToTask : SomeTask
    {

        [SerializeField]
        public TaskSource target;
        
        protected override string AutoName => $"Jump to {target.gameObject.name}";


        public override UniTask<Scope> Run(Scope scope)
        {
//            await target.task.Run(scope);
            
            scope.Add(target.task);

            return UniTask.FromResult(scope);
        }

    }
}

using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class Detour : MetaTask
    {

        [SerializeField]
        public MetaTaskInstance target;
        
        protected override bool CanAutoName() => target;


        protected override string CreateAutoName() => $"Divert to [{target.Task.Name}]";


//        protected override async UniTask<Tracer> _Run(Tracer tracer) => await target.Task.Run(tracer);
        protected override async UniTask<Tracer> _Run(Tracer tracer) => await target.Detour(tracer);

    }
}

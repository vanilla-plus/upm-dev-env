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
        
        protected override bool CanDescribe() => target;


        protected override string DescribeTask() => $"Divert to [{target.TaskSet.Name}]";


        protected override UniTask _Run(CancellationTokenSource cancellationTokenSource) => target.TaskSet.Run(cancellationTokenSource);

    }
}

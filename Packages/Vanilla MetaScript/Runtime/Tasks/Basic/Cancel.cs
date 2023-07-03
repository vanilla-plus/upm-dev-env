using System;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class Cancel : MetaTask
    {

        protected override bool CanDescribe() => true;
        
        protected override string DescribeTask() => "Cancel the current thread";

        protected override UniTask _Run(CancellationTokenSource cancellationTokenSource)
        {
            cancellationTokenSource?.Cancel();

            return default;
        }

    }
}

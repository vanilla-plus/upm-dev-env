using System;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class Cancel : MetaTask
    {

        protected override bool CanAutoName() => true;
        
        protected override string CreateAutoName() => "Cancel the current thread";

        protected override UniTask<Tracer> _Run(Tracer tracer)
        {
            tracer.Continue = false;

            return UniTask.FromResult(tracer);
        }

    }
}

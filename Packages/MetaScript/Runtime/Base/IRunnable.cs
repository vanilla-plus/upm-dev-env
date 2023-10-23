using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript
{
    public interface IRunnable
    {

        public UniTask<ExecutionTrace> Run(ExecutionTrace trace);

    }
//
//    public abstract class MetaTask2
//    {
//
//        public async UniTask<(MetaTask2,trace)> Run(trace trace)
//        {
////            await UniTask.Yield();
//            
//            return (this, trace);
//        }
//
//        protected abstract UniTask<trace> _Run(trace trace);
//
//    }
}

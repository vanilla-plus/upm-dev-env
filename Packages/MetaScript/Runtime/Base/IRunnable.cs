using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript
{
    public interface IRunnable
    {

        public UniTask<Tracer> Run(Tracer tracer);

    }
//
//    public abstract class MetaTask2
//    {
//
//        public async UniTask<(MetaTask2,Tracer)> Run(Tracer tracer)
//        {
////            await UniTask.Yield();
//            
//            return (this, tracer);
//        }
//
//        protected abstract UniTask<Tracer> _Run(Tracer tracer);
//
//    }
}

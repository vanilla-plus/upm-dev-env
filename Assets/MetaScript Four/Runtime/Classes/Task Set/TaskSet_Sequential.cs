using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.Four
{

    public class TaskSet_Sequential : TaskSet_Base
    {

        public override async UniTask Run()
        {
            foreach (var t in tasks)
            {
                if (ReferenceEquals(objA: t,
                                    objB: null)) continue;

                if (t.async)
                {
                    t.Run();
                }
                else
                {
                    await t.Run();
                }
            }
        }

    }

}
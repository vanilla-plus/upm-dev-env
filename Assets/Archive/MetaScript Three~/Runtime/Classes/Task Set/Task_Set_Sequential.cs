using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.Three
{

    public class Task_Set_Sequential : Task_Set
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
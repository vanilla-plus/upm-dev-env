using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Four
{

    public interface ICondition
    {

        bool Resolve();

    }

    [Serializable]
    public struct Task_BinaryBranch : ITask
    {

        [SerializeField]
        private bool _async;

        public bool async
        {
            get => _async;
            set => _async = value;
        }

        [SerializeReference] [TypeMenu]
        public ICondition[] conditions;

        [SerializeReference] [TypeMenu]
        public ITask trueTask;

        [SerializeReference] [TypeMenu]
        public ITask falseTask;

        public void Validate() { }


        public async UniTask Run()
        {
            foreach (var c in conditions)
            {
                if (c.Resolve()) continue;

                if (falseTask.async)
                {
                    falseTask.Run();
                }
                else
                {
                    await falseTask.Run();
                }

                return;
            }

            if (ReferenceEquals(objA: trueTask,
                                objB: null)) return;

            if (trueTask.async)
            {
                trueTask.Run();
            }
            else
            {
                await trueTask.Run();
            }
        }

    }

}
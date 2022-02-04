using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Three
{

    [Serializable]
    public abstract class Task_Set : ITask
    {

        [SerializeReference] [TypeMenu]
        public ITask[] tasks = new ITask[0];

        public virtual void Validate()
        {
            #if UNITY_EDITOR
            foreach (var t in tasks)
            {
                t?.Validate();
            }
            #endif
        }


        [SerializeField]
        private bool _async;
        public bool async
        {
            get => _async;
            set => _async = value;
        }

        public abstract UniTask Run();

    }

}
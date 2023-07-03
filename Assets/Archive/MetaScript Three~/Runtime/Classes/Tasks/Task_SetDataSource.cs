using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Three
{
    public class Task_SetDataSource : ITask
    {

        public void Validate() { }

        [SerializeField]
        private bool _async;
        public bool async
        {
            get => _async;
            set => _async = value;
        }

//        [SerializeReference]
//        [TypeMenu]
        
        public UniTask Run() => default;

    }
}

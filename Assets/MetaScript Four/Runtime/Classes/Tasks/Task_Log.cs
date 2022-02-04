using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.DataAssets.Three;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Four
{

    [Serializable]
    public struct Task_Log : ITask
    {

        public void Validate() { }

        [SerializeField]
        private bool _async;
        public bool async
        {
            get => _async;
            set => _async = value;
        }

        [SerializeReference]
        [TypeMenu]
        public StringSource message;

        public UniTask Run()
        {
            Debug.Log(message.Get());

            return default;
        }

    }

}
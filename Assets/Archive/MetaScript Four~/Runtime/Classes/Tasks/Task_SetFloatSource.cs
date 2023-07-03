using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.DataAssets.Three;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Four
{
    [Serializable]
    public struct Task_SetFloatSource : ITask
    {

        public void Validate()
        {
            source?.Validate();
            target?.Validate();
        }

        [SerializeField]
        private bool _async;
        public bool async
        {
            get => _async;
            set => _async = value;
        }

        [SerializeReference]
        [TypeMenu]
        public FloatSource source;

        [SerializeReference]
        [TypeMenu]
        public FloatSource target;
        
        public async UniTask Run() => source.Set(target.Get());

    }
}

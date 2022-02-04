using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.Four
{
    [Serializable]
    public class TaskSetPlayer_OnInitialize : TaskSetPlayer_Base, IInitiable
    {

        [SerializeField]
        private bool _initialized;
        public bool Initialized => _initialized;

        public async UniTask Initialize() => await Play();

        public UniTask DeInitialize() => default;

    }
}

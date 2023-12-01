using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.Debugging
{

    [Serializable]
    public class Key_Down : MetaTask
    {

        [SerializeField]
        public KeyCode key = KeyCode.Alpha1;

        protected override bool CanAutoName() => true;


        protected override string CreateAutoName() => $"Wait for [{key}] key press";


        protected override async UniTask<Scope> _Run(Scope scope)
        {
            do
            {
                if (scope.Cancelled) return scope;

                await UniTask.Yield();
            }
            while (!Input.GetKeyDown(key));
            
            return scope;
        }

    }

}

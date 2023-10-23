using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

    [Serializable]
    public class Await_Key_Down : MetaTask
    {

        [SerializeField]
        public KeyCode key = KeyCode.Alpha1;

        protected override bool CanAutoName() => true;


        protected override string CreateAutoName() => $"Wait for [{key}] key press";


        protected override async UniTask<Scope> _Run(Scope scope)
        {
            do
            {
                if (scope.Cancelled)
                {
//                    Debug.LogWarning($"Yikes! I'm outta here [{key}]");
                    
                    return scope;
                }

                await UniTask.Yield();
            }
            while (!Input.GetKeyDown(key));
            
            return scope;
        }

    }

}

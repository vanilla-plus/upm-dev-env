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


        protected override async UniTask<ExecutionTrace> _Run(ExecutionTrace trace)
        {
            do
            {
                if (trace.Cancelled) return trace;
//                if (trace.HasBeenCancelled(this)) return trace;

                await UniTask.Yield();
            }
            while (!Input.GetKeyDown(key));
            
            return trace;
        }

    }

}

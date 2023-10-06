using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

    [Serializable]
    public class AwaitKeyDown : MetaTask
    {

        [SerializeField]
        public KeyCode key = KeyCode.Alpha1;

        protected override bool CanAutoName() => true;


        protected override string CreateAutoName() => $"Wait for [{key}] key press";


        protected override async UniTask<Tracer> _Run(Tracer tracer)
        {
            do
            {
                if (tracer.Cancelled(this)) return tracer;

                await UniTask.Yield();
            }
            while (!Input.GetKeyDown(key));
            
            return tracer;
        }

    }

}

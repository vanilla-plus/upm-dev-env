using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Easing;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public abstract class Lerp : MetaTask
    {

        [SerializeField]
        public float secondsToTake = 1.0f;

        [TypeMenu]
        [SerializeReference]
        public IEasingSlot easingSlot = new Linear();

        protected override async UniTask<Scope> _Run(Scope scope)
        {
            var t    = 0.0f;
            var rate = 1.0f / secondsToTake;

            Init();

            do
            {
                if (scope.Cancelled) return scope;

                t += Time.deltaTime * rate;

                Frame(normal: t,
                      easedNormal: easingSlot.Ease(t));

                await UniTask.Yield();
            }
            while (t < 1.0f);

            CleanUp();

            return scope;
        }


        protected abstract void Init();

        protected abstract void Frame(float normal,
                                      float easedNormal);


        protected abstract void CleanUp();

    }
}

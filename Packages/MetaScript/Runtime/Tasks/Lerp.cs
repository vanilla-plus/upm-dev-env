using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public abstract class Lerp : MetaTask
    {

        [SerializeField]
        public float secondsToTake = 1.0f;
        
        [SerializeField]
        public AnimationCurve easingCurve = AnimationCurve.Linear(0.0f,
                                                                  0.0f,
                                                                  1.0f,
                                                                  1.0f);

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
                      easedNormal: easingCurve.Evaluate(t));

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

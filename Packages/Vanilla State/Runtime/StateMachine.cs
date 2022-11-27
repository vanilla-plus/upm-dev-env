#if DEVELOPMENT_BUILD && VANILLA && STATE
#define debug
#endif

using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.SmartState
{
    [Serializable]
    public class StateMachine : SmartState
    {

        public StateMachine(SmartBool activeCondition) : base(activeCondition) { }

        public StateMachine(bool startingState) : base(startingState) { }
        
        [SerializeField]
        public SmartState previous;
        [SerializeField]
        public SmartState current;
        [SerializeField]
        public SmartState next;

        public async UniTask Request(SmartState incomingState)
        {
            if (ReferenceEquals(incomingState,
                                current))
            {
                #if !debug
                Debug.Log($"Requested state [{incomingState}] is already the current state [{current}]");
                #endif
                
                return;
            }

            next = incomingState;

            if (current != null)
            {
                current.Active.Value = false;

                while (!current.Progress.AtMin) await UniTask.Yield();
            }

            previous = current;

            current = next;

            next = null;

            if (current != null)
            {
                current.Active.Value = true;
            }
        }


        public async UniTask GoBack()
        {
            if (previous != null)
            {
                await Request(previous);
            }
        }

    }
}

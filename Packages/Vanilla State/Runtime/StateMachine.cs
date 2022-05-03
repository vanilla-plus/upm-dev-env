#if DEVELOPMENT_BUILD && VANILLA && STATE
#define debug
#endif

using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.State
{
    [Serializable]
    public class StateMachine : State
    {

        public StateMachine(Toggle activeCondition) : base(activeCondition) { }

        public StateMachine(bool startingState) : base(startingState) { }
        
        [SerializeField]
        public State previous;
        [SerializeField]
        public State current;
        [SerializeField]
        public State next;

        public async UniTask Request(State incomingState)
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
                current.active.State = false;

                while (!current.activeNormal.Empty) await UniTask.Yield();
            }

            previous = current;

            current = next;

            next = null;

            if (current != null)
            {
                current.active.State = true;
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

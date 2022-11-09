using System;

using UnityEngine;

namespace Vanilla.State
{

    [Serializable]
    public class State
    {

        [SerializeField]
        [HideInInspector]
        private bool defaultActiveState;
        
        [SerializeField]
        public Toggle active;

        [SerializeField]
        public Normal activeNormal = new Normal(0.0f);

        [SerializeField]
        public float activeTransitionSpeed = 1.0f;

        [SerializeField]
        public float inactiveTransitionSpeed = 1.0f;
        
        [NonSerialized]
        private bool _initialized = false;


        public State(Toggle activeCondition)
        {
            active = activeCondition;

            activeNormal.Value = active.State ?
                                     1.0f :
                                     0.0f;
        }


        public State(bool startingState)
        {
            active = new Toggle(startingState);

            activeNormal.Value = startingState ?
                                     1.0f :
                                     0.0f;
        }


        public void OnValidate()
        {
            active.OnValidate();
            activeNormal.OnValidate();

            defaultActiveState = active.State;
        }


        public void Init()
        {
            if (_initialized) return;

            _initialized = true;
            
            active.onTrue += Fill;

            active.onFalse += Drain;
        }


        private void Fill() => activeNormal.Fill(conditional: active,
                                                 targetCondition: true,
                                                 speed: activeTransitionSpeed);


        private void Drain() => activeNormal.Drain(conditional: active,
                                                   targetCondition: false,
                                                   speed: inactiveTransitionSpeed);


        public void DeInit()
        {
            if (!_initialized) return;

            _initialized  =  false;
            
            active.onTrue -= Fill;

            active.onFalse -= Drain;
        }


        public void Reset()
        {
            active.State = defaultActiveState;
            
            activeNormal.Value = defaultActiveState ? 1.0f : 0.0f;
            
            active.SilentSet(defaultActiveState);

            activeNormal.SilentSet(defaultActiveState ?
                                       1.0f :
                                       0.0f);
        }


        public virtual StateMachine Machine => null;

    }

}
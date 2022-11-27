using System;

using UnityEngine;

namespace Vanilla.SmartState
{

    [Serializable]
    public class SmartState
    {

        [SerializeField]
        [HideInInspector]
        private bool defaultActiveState;
        
        [SerializeField]
        public SmartBool Active;

        [SerializeField]
        public SmartFloat Progress = new SmartFloat(0.0f);

        [SerializeField]
        public float activeTransitionRate = 1.0f;
        
        [SerializeField]
        public float activeTransitionSpeed = 1.0f;

        [SerializeField]
        public float inactiveTransitionRate = 1.0f;

        [SerializeField]
        public float inactiveTransitionSpeed = 1.0f;

        [NonSerialized]
        private bool _initialized = false;


        public SmartState(SmartBool activeCondition)
        {
            Active = activeCondition;

            Progress.Value = Active.Value ?
                                     1.0f :
                                     0.0f;
        }


        public SmartState(bool startingState)
        {
            Active = new SmartBool(startingState);

            Progress.Value = startingState ?
                                     1.0f :
                                     0.0f;
        }


        public void OnValidate()
        {
            Active.OnValidate();
            Progress.OnValidate();

            defaultActiveState = Active.Value;
        }


        public void Init()
        {
            if (_initialized) return;

            _initialized = true;
            
            Active.onTrue += Fill;

            Active.onFalse += Drain;
        }


        private void Fill() => Progress.Fill(conditional: Active,
                                                     targetCondition: true,
                                                     amountPerSecond: activeTransitionRate,
                                                     secondsToTake: activeTransitionSpeed);


        private void Drain() => Progress.Drain(conditional: Active,
                                                   targetCondition: false,
                                                   amountPerSecond: inactiveTransitionRate, 
                                                   secondsToTake: inactiveTransitionSpeed);


        public void DeInit()
        {
            if (!_initialized) return;

            _initialized  =  false;
            
            Active.onTrue -= Fill;

            Active.onFalse -= Drain;
        }


        public void Reset()
        {
            Active.Value = defaultActiveState;
            
            Progress.Value = defaultActiveState ? 1.0f : 0.0f;
            
            Active.SilentSet(defaultActiveState);

            Progress.SilentSet(defaultActiveState ?
                                       1.0f :
                                       0.0f);
        }


        public virtual StateMachine Machine => null;

    }

}
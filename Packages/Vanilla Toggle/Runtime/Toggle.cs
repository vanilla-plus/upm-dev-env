using System;

using UnityEngine;

namespace Vanilla
{

    [Serializable]
    public class Toggle
    {

        [SerializeField]
        private bool _state;
        public bool State
        {
            get => _state;
            set
            {
                if (_state == value) return;

                _state = value;

                onChange?.Invoke(_state);

                if (_state)
                {
                    onTrue?.Invoke();
                }
                else
                {
                    onFalse?.Invoke();
                }
            }
        }

        public Action<bool> onChange;

        public Action onTrue;
        public Action onFalse;

        public Toggle(bool startingState) => _state = startingState;


        public static implicit operator bool(Toggle toggle) => toggle is
                                                               {
                                                                   State: true
                                                               };


        public void Flip() => State = !_state;

    }

}
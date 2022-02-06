using System;

using UnityEngine;

namespace Vanilla
{

    [Serializable]
    public class Toggle
    {

        [SerializeField]
        private bool _true;
        public bool True
        {
            get => _true;
            set
            {
                if (_true == value) return;

                _true = value;

                onChange?.Invoke(_true);

                if (_true)
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

        public Toggle(bool startingState) => _true = startingState;


        public static implicit operator bool(Toggle toggle) => toggle is
                                                               {
                                                                   True: true
                                                               };

    }

}
using System;

using UnityEngine;

namespace Vanilla.MenuMachine
{
    [Serializable]
    public class Toggle
    {

        [SerializeField]
        internal bool _active;
        public bool active
        {
            get => _active;
            set
            {
                if (_active == value) return;

                _active = value;

                onChange?.Invoke(_active);
            }
        }

        public Action<bool> onChange;

        public Action     onTrue;
        public Action     onFalse;

    }
}
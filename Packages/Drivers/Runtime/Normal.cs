using System;

using UnityEngine;

namespace Vanilla.Drivers
{

    [Serializable]
    public class Normal
    {

        [Range(0.0f, 1.0f)]
        [SerializeField]
        private float _Value = 0.0f;
        public float Value
        {
            get => _Value;
            set
            {
                value = Mathf.Clamp01(value);
                
                if (Math.Abs(_Value - value) < changeEpsilon) return;

                // There has been a significant change!

                _Value = value;

                OnValueChanged?.Invoke(_Value);
            }
        }

        [SerializeField]
        public float changeEpsilon = float.Epsilon;

        [SerializeField]
        public float minMaxEpsilon = 0.01f;

        [NonSerialized]
        public Action<float> OnValueChanged;

        public bool ValueAtMin => _Value                         < minMaxEpsilon;
        public bool ValueAtMax => Math.Abs(value: _Value - 1.0f) < minMaxEpsilon;

        public static implicit operator float(Normal input) => input?.Value ?? 0f;

    }

}

using System;

using UnityEngine;

namespace Vanilla.Drivers
{

    [Serializable]
    public class Normal
    {

        [Range(0.0f, 1.0f)]
        [SerializeField]
        private float _Value;
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

        [HideInInspector]
        [SerializeField]
        public float changeEpsilon;

        [HideInInspector]
        [SerializeField]
        public float minMaxEpsilon;

        [NonSerialized]
        public Action<float> OnValueChanged;

        public bool ValueAtMin => _Value                         < minMaxEpsilon;
        public bool ValueAtMax => Math.Abs(value: _Value - 1.0f) < minMaxEpsilon;

        public static implicit operator float(Normal input) => input?.Value ?? 0f;


        public void OnValidate()
        {
            if (changeEpsilon == 0) changeEpsilon = float.Epsilon;
            if (minMaxEpsilon == 0) minMaxEpsilon = 0.01f;
        }

    }

}

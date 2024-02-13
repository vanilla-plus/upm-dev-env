using System;

using UnityEngine;

namespace Vanilla.DataSources
{

    [Serializable]
    public class ProtectedRangedFloatSource : FloatSource,
                                              IProtectedSource<float>,
                                              IRangedDataSource<float>
    {

        [SerializeField]
        public float _changeEpsilon = Mathf.Epsilon;
        public float ChangeEpsilon
        {
            get => _changeEpsilon;
            set => _changeEpsilon = value;
        }


        [SerializeField]
        private float _value = 0.0f;
        public override float Value
        {
            get => _value;
            set
            {
                var incoming = Mathf.Clamp(value,
                                           Min,
                                           Max);
                
                if (Mathf.Abs(_value - incoming) > ChangeEpsilon) return;

                var old = _value;

                _value = incoming;

                if (_value > old)
                {
                    _atMin.Value = Math.Abs(value: _value - _Min) < MinMaxEpsilon;

                    OnSet?.Invoke(_value);

                    OnSetWithHistory?.Invoke(_value,
                                             old);
                    
                    _atMax.Value = Math.Abs(value: _value - _Max) < MinMaxEpsilon;
                }
                else
                {
                    _atMax.Value = Math.Abs(value: _value - _Max) < MinMaxEpsilon;

                    OnSet?.Invoke(_value);

                    OnSetWithHistory?.Invoke(_value,
                                             old);
                    
                    _atMin.Value = Math.Abs(value: _value - _Min) < MinMaxEpsilon;
                }
            }
        }

        [SerializeField]
        private float _Min = float.MinValue;
        public float Min
        {
            get => _Min;
            set => _Min = value;
        }

        [SerializeField]
        private float _Max = float.MaxValue;
        public float Max
        {
            get => _Max;
            set => _Max = value;
        }

        [SerializeField]
        private ProtectedBoolSource _atMin = new ProtectedBoolSource();
        public ProtectedBoolSource AtMin
        {
            get => _atMin;
            set => _atMin = value;
        }

        [SerializeField]
        private ProtectedBoolSource _atMax = new ProtectedBoolSource();
        public ProtectedBoolSource AtMax
        {
            get => _atMax;
            set => _atMax = value;
        }

        [SerializeField]
        private float _minMaxEpsilon = 0.0001f;
        public float MinMaxEpsilon
        {
            get => _minMaxEpsilon;
            set => _minMaxEpsilon = value;
        }

        public override void OnBeforeSerialize() { }

        public override void OnAfterDeserialize()
        {
            _Min = Mathf.Clamp(_Min,
                               float.MinValue,
                               _Max);

            _Max = Mathf.Clamp(_Max,
                               _Min,
                               float.MaxValue);

//            AtMin.Name = $"{Name}.AtMin";
//            AtMax.Name = $"{Name}.AtMax";

            Value = Mathf.Clamp(_value,
                                _Min,
                                _Max);
        }

    }

}
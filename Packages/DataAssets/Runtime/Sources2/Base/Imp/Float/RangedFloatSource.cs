using System;

using UnityEngine;

namespace Vanilla.DataSources
{

    [Serializable]
    public class RangedFloatSource : FloatSource,
                                     IRangedDataSource<float>
    {

        [SerializeField]
        private float _value;
        public override float Value
        {
            get => _value;
            set
            {
                var old = _value;

                _value = Mathf.Clamp(value,
                                     Min,
                                     Max);

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
        public ProtectedBoolSource AtMin => _atMin;

        [SerializeField]
        private ProtectedBoolSource _atMax = new ProtectedBoolSource();
        public ProtectedBoolSource AtMax => _atMax;
        
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

            AtMin.Name = $"{Name}.AtMin";
            AtMax.Name = $"{Name}.AtMax";
            
            Value = Mathf.Clamp(_value,
                                 _Min,
                                 _Max);
        }

    }

}
using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{

    [Serializable]
    public class ProtectedRangedFloatSource : FloatSource,
                                              IProtectedSource<float>,
                                              IRangedDataSource<float>
    {

        [SerializeField]
        private string _name = "Unnamed ProtectedRangedFloatSource";
        public string Name
        {
            get => _name;
            set => _name = value;
        }

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
                value = Mathf.Clamp(value,
                                    Min,
                                    Max);

                if (Mathf.Abs(_value - value) < ChangeEpsilon) return;
                
                var outgoing = _value;

                _value = value;

                #if debug
                Debug.Log($"[{Name}] was changed from [{outgoing}] to [{value}]");
                #endif

                if (value > outgoing)
                {
                    _atMin.Value = Math.Abs(value: value - _Min) < MinMaxEpsilon;

                    OnSet?.Invoke(value);

                    OnSetWithHistory?.Invoke(value,
                                             outgoing);
                    
                    _atMax.Value = Math.Abs(value: _value - _Max) < MinMaxEpsilon;
                }
                else
                {
                    _atMax.Value = Math.Abs(value: value - _Max) < MinMaxEpsilon;

                    OnSet?.Invoke(value);

                    OnSetWithHistory?.Invoke(value,
                                             outgoing);
                    
                    _atMin.Value = Math.Abs(value: value - _Min) < MinMaxEpsilon;
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

            AtMin.Name = $"{Name}.AtMin";
            AtMax.Name = $"{Name}.AtMax";
            
//            Value = Mathf.Clamp(_value,
//                                 _Min,
//                                 _Max);

            Value = _value;
        }

//
//        public override void OnAfterDeserialize() => ValidateRangedValue();
//
//
//        public void ValidateRangedValue()
//        {
//            ValidateMin();
//            ValidateMax();
//
//            ValidateValue();
//
//            AtMin.Name = $"{Name}.AtMin";
//
//            ValidateAtMin();
//
//            AtMax.Name = $"{Name}.AtMax";
//
//            ValidateAtMax();
//        }
//
//
//        private void ValidateMin() => _Min = Mathf.Clamp(_Min,
//                                                        float.MinValue,
//                                                        max: _Max);
//
//
//        private void ValidateMax() => _Max = Mathf.Clamp(_Max,
//                                                         min: _Min,
//                                                         max: float.MaxValue);
//
//
//        private void ValidateValue() => Value = Mathf.Clamp(_value,
//                                                             min: _Min,
//                                                             max: _Max);
//
//
//        protected void ValidateAtMin() => AtMin.Value = Math.Abs(value: _value - _Min) < MinMaxEpsilon;
//
//        protected void ValidateAtMax() => AtMax.Value = Math.Abs(value: _value - _Max) < MinMaxEpsilon;


    }

}
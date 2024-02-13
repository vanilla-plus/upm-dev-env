using System;

using UnityEngine;

namespace Vanilla.DataSources
{

    [Serializable]
    public class RangedIntSource : IntSource,
                                   IRangedDataSource<int>
    {

        [SerializeField]
        private int _value;
        public override int Value
        {
            get => _value;
            set
            {
                var old = _value;

                _value = Mathf.Clamp(value: value,
                                     min: Min,
                                     max: Max);

                if (_value > old)
                {
                    _atMin.Value = _value == _Min;

                    OnSet?.Invoke(_value);

                    OnSetWithHistory?.Invoke(_value,
                                             old);
                    
                    _atMax.Value = _value == Max;
                }
                else
                {
                    _atMax.Value = _value == Max;

                    OnSet?.Invoke(_value);

                    OnSetWithHistory?.Invoke(_value,
                                             old);
                    
                    _atMin.Value = _value == _Min;
                }
            }
        }

        [SerializeField]
        private int _Min = int.MinValue;
        public int Min
        {
            get => _Min;
            set => _Min = value;
        }

        [SerializeField]
        private int _Max = int.MaxValue;
        public int Max
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

        public override void OnBeforeSerialize() { }


        public override void OnAfterDeserialize()
        {
            _Min = Mathf.Clamp(value: _Min,
                               min: int.MinValue,
                               max: _Max);

            _Max = Mathf.Clamp(value: _Max,
                               min: _Min,
                               max: int.MaxValue);

//            AtMin.Name = $"{Name}.AtMin";
//            AtMax.Name = $"{Name}.AtMax";
            
            Value = Mathf.Clamp(value: _value,
                                 min: _Min,
                                 max: _Max);
        }

    }

}
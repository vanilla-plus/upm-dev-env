using System;

using UnityEngine;

namespace Vanilla.DataSources
{

    [Serializable]
    public class ProtectedRangedIntSource : IntSource,
                                              IProtectedSource<int>,
                                              IRangedDataSource<int>
    {

        [SerializeField]
        private int _value;
        public override int Value
        {
            get => _value;
            set
            {
                var incoming = Mathf.Clamp(value,
                                           Min,
                                           Max);
                
                if (_value == incoming) return;

                var old = _value;

                _value = incoming;
                
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
            _Min = Mathf.Clamp(_Min,
                               int.MinValue,
                               _Max);

            _Max = Mathf.Clamp(_Max,
                               _Min,
                               int.MaxValue);

//            AtMin.Name = $"{Name}.AtMin";
//            AtMax.Name = $"{Name}.AtMax";
            
            Value = Mathf.Clamp(_value,
                                 _Min,
                                 _Max);
        }

    }

}
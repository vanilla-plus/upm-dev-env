using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{

    [Serializable]
    public class ProtectedRangedIntSource : IntSource,
                                              IProtectedSource<int>,
                                              IRangedDataSource<int>
    {

        [SerializeField]
        private string _name = "Unnamed ProtectedRangedColorSource";
        public string Name
        {
            get => _name;
            set => _name = value;
        }



        [SerializeField]
        private int _value;
        public override int Value
        {
            get => _value;
            set
            {
                value = Mathf.Clamp(value,
                                    Min,
                                    Max);
                
                if (_value == value) return;

                var outgoing = _value;

                _value = value;

                #if debug
                Debug.Log($"[{Name}] was changed from [{outgoing}] to [{value}]");
                #endif
                
                if (value > outgoing)
                {
                    _atMin.Value = value == _Min;

                    OnSet?.Invoke(value);

                    OnSetWithHistory?.Invoke(value,
                                             outgoing);
                    
                    _atMax.Value = value == Max;
                }
                else
                {
                    _atMax.Value = value == Max;

                    OnSet?.Invoke(value);

                    OnSetWithHistory?.Invoke(value,
                                             outgoing);
                    
                    _atMin.Value = value == _Min;
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

            AtMin.Name = $"{Name}.AtMin";
            AtMax.Name = $"{Name}.AtMax";

            Value = _value;

//            var old = _value;

//            _value = Mathf.Clamp(_value,
//                                 _Min,
//                                 _Max);
            
//            SignificantSet(_value, old);
        }

    }

}
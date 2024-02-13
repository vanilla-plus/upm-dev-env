using System;

using UnityEngine;

namespace Vanilla.DataSources
{
    
    [Serializable]
    public class RangedVec2Source : Vec2Source, IRangedDataSource<Vector2>
    {

        [SerializeField]
        private Vector2 _value;
        public override Vector2 Value
        {
            get => _value;
            set
            {
                value = new Vector2(x: Mathf.Clamp(value: value.x,
                                                   min: _min.x,
                                                   max: _max.x),
                                    y: Mathf.Clamp(value: value.y,
                                                   min: _min.y,
                                                   max: _max.y));
                
                var old = _value;

                _value = value;
                
                // Hm, we don't check for an 'increase' or 'decrease' here because I suppose it's a little bit vague when it comes to Vectors?
                // The way more useful information here would be if each particular dimension had its own Min/Max/AtMin/AtMax but structuring that
                // would be hell...?
                
                // I think we can flub this check for now.

                OnSet?.Invoke(_value);
                OnSetWithHistory?.Invoke(arg1: _value, arg2: old);
                
                _atMin.Value = VectorIsAtMin;
                _atMax.Value = VectorIsAtMax;
            }
        }

        [SerializeField]
        private Vector2 _min = new Vector2(x: float.MinValue,
                                           y: float.MinValue);
        public Vector2 Min
        {
            get => _min;
            set => _min = value;
        }

        [SerializeField]
        private Vector2 _max = new Vector2(x: float.MaxValue,
                                           y: float.MaxValue);
        public Vector2 Max
        {
            get => _max;
            set => _max = value;
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
            _min = new Vector2(x: Mathf.Clamp(value: _min.x,
                                              min: float.MinValue,
                                              max: _max.x),
                               y: Mathf.Clamp(value: _min.y,
                                              min: float.MinValue,
                                              max: _max.y));
            
            _max = new Vector2(x: Mathf.Clamp(value: _max.x,
                                              min: _min.x,
                                              max: float.MaxValue),
                               y: Mathf.Clamp(value: _max.y,
                                              min: _min.y,
                                              max: float.MaxValue));

            AtMin.Name = $"{Name}.AtMin";
            AtMax.Name = $"{Name}.AtMax";
            
            Value = new Vector2(x: Mathf.Clamp(value: _value.x,
                                                min: _min.x,
                                                max: _max.x),
                                 y: Mathf.Clamp(value: _value.y,
                                                min: _min.y,
                                                max: _max.y));
        }


        private bool VectorIsAtMin => Mathf.Abs(_value.x - _min.x) < MinMaxEpsilon && Mathf.Abs(_value.y - _min.y) < MinMaxEpsilon;
        private bool VectorIsAtMax => Mathf.Abs(_value.x - _max.x) < MinMaxEpsilon && Mathf.Abs(_value.y - _max.y) < MinMaxEpsilon;

    }
}

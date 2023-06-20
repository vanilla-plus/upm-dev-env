#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.SmartValues
{
    
    [Serializable]
    public abstract class SmartRangedValue<T> : SmartStruct<T> where T : struct 
    {

        
        #region Properties

        [SerializeField]
        protected T _Min;
        public T Min
        {
            get => _Min;
            set
            {
                // Make sure the incoming Min value is within a valid range
                // i.e. greater than the generic types MinimumValue but less than whatever Max currently is
                value = GetClamped(value,
                              min: TypeMinValue,
                              max: _Max);

                _Min = value;

                // if Min has risen above Value, bring Value up with it

                Value = GetGreater(Value,
                                _Min);
            }
        }
        
        [SerializeField]
        protected T _Max;
        public T Max
        {
            get => _Max;
            set
            {
                // Make sure the incoming Max value is within a valid range
                // i.e. greater than whatever Min currently is but less than the generic types MaximumValue

                value = GetClamped(value,
                                   _Min,
                                   TypeMaxValue);

                _Max = value;

                // if Max has dropped below Value, bring Value down with it
                
                Value = GetLesser(Value,
                                _Max);
            }
        }
        
        
        public override T Value
        {
            get => _Value;
            set
            {
                // First of all, make sure the incoming value is within the allowed range between Min and Max.
                // This allows us to accurately defend against false change attempts at these upper and lower bounds.
                value = GetClamped(value,
                                  Min,
                                  Max);

                // Now we can compare for change similarity.
                // This step works best thanks to the above clamp!
                if (ValueEquals(_Value,
                                value)) return;
                
                // There has been a significant change!
                
                // Cache the outgoing value
                var old = _Value;

                _Value = value;
                
                #if debug
				Debug.Log($"[{Name}] changed from [{old}] to [{value}]");
                #endif

                // If the outgoing value is smaller than the incoming value...
                if (LessThan(old, _Value))
                {
                    // It was an increase!
                    
                    AtMin.Value = ValueAtMin(); // Set AtMin

                    OnValueChanged?.Invoke(_Value,
                                           old);

                    onIncrease?.Invoke(_Value,
                                       old);

                    AtMax.Value = ValueAtMax(); // Set AtMax
                }
                else
                {
                    // It was an decrease!

                    AtMax.Value = ValueAtMax(); // Set AtMax

                    OnValueChanged?.Invoke(_Value,
                                           old);

                    onDecrease?.Invoke(_Value,
                                       old);

                    AtMin.Value = ValueAtMin(); // Set AtMin
                }
            }
        }
        
        [SerializeField]
        public SmartBool AtMin = new("AtMin",
                                     false);

        [SerializeField]
        public SmartBool AtMax = new("AtMax",
                                     false);

        [NonSerialized]
        public Action<T, T> onDecrease;

        [NonSerialized]
        public Action<T, T> onIncrease;
        
        #endregion
        
        #region Serialization


        public override void OnBeforeSerialize() { }


        public override void OnAfterDeserialize()
        {
            ValidateMin();
            ValidateMax();

            ValidateValue();

            AtMin.Name = $"{Name}.AtMin";

            ValidateAtMin();

            AtMax.Name = $"{Name}.AtMax";

            ValidateAtMax();
        }


        private void ValidateMin() => _Min = GetClamped(_Min,
                                                        TypeMinValue,
                                                        _Max);


        private void ValidateMax() => _Max = GetClamped(_Max,
                                                         _Min,
                                                         TypeMaxValue);


        private void ValidateValue() => _Value = GetClamped(_Value,
                                                            _Min,
                                                            _Max);

        protected void ValidateAtMin() => AtMin._Value = ValueAtMin();

        protected void ValidateAtMax() => AtMax._Value = ValueAtMax();
        

        #endregion

        #region Construction

        public SmartRangedValue() { }

        public SmartRangedValue(string name) : base(name) { }


        public SmartRangedValue(string defaultName,
                                T defaultValue) : base(defaultName,
                                                       defaultValue) { }


        public SmartRangedValue(string defaultName,
                                T defaultValue,
                                T defaultMin,
                                T defaultMax) : base(defaultName,
                                                     defaultValue)
        {
            // Before running the setter validations, set everything to its default values.
            // This allows Min to use Max, Max to use Min and Value to use both properly.
            
            _Value = defaultValue;
            _Min   = defaultMin;
            _Max   = defaultMax;

            Min   = _Min;
            Max   = _Max;
            Value = _Value;
        }



        #endregion
        
        #region Math
        
        public abstract T TypeMinValue { get; }
        public abstract T TypeMaxValue { get; }

        public abstract T GetClamped(T input,
                                    T min,
                                    T max);


        public abstract T GetLesser(T input,
                                  T other);


        public abstract T GetGreater(T input,
                                  T other);
        
        public abstract bool LessThan(T a,
                                      T b);

        public abstract bool GreaterThan(T a,
                                         T b);

        
        // Thanks to the nature of floating point inprecision, they handle AtMin and AtMax state
        // completely differently (they get a separate epsilon all to themselves).
        
        public abstract bool ValueAtMin();
        public abstract bool ValueAtMax();



        #endregion

    }

    [Serializable]
    public class SmartFloat : SmartRangedValue<float>
    {

        #region Properties
        
        [SerializeField]
        public float ChangeEpsilon = Mathf.Epsilon;
		
		[SerializeField]
		public float MinMaxEpsilon = 0.01f;


        public SmartFloat(string defaultName,
                          float defaultValue,
                          float defaultMin,
                          float defaultMax,
                          float changeEpsilon) : base(defaultName,
                                                      defaultValue,
                                                      defaultMin,
                                                      defaultMax) => ChangeEpsilon = changeEpsilon;


        public SmartFloat(string defaultName,
                          float defaultValue,
                          float defaultMin,
                          float defaultMax,
                          float changeEpsilon,
                          float minMaxEpsilon) : base(defaultName,
                                                      defaultValue,
                                                      defaultMin,
                                                      defaultMax)
        {
            ChangeEpsilon = changeEpsilon;
            MinMaxEpsilon = minMaxEpsilon;
        }



        #endregion

        #region Overrides
        
        public override bool ValueEquals(float a,
                                         float b) => Mathf.Abs(a - b) < ChangeEpsilon;

        #endregion
        
        #region Math

        public override float TypeMinValue => float.MinValue;
        public override float TypeMaxValue => float.MaxValue;


        public override float GetClamped(float input,
                                         float min,
                                         float max) => Math.Clamp(input,
                                                                  min,
                                                                  max);


        public override float GetLesser(float input,
                                        float other) => Math.Min(input,
                                                                 other);


        public override float GetGreater(float input,
                                         float other) => Math.Max(input,
                                                                  other);


        public override bool LessThan(float a,
                                      float b) => a < b;


        public override bool GreaterThan(float a,
                                         float b) => a > b;


        public override bool ValueAtMin() => Mathf.Abs(_Value - _Min) < MinMaxEpsilon;


        public override bool ValueAtMax() => Mathf.Abs(_Value - _Max) < MinMaxEpsilon;
        
        #endregion

    }
    
    
    [Serializable]
    public class SmartInt : SmartRangedValue<int>
    {

        public SmartInt(string defaultName,
                        int defaultValue,
                        int defaultMin,
                        int defaultMax) : base(defaultName,
                                               defaultValue,
                                               defaultMin,
                                               defaultMax) { }


        public override bool ValueEquals(int a,
                                         int b) => a == b;

        #region Math
        public override int TypeMinValue => int.MinValue;
        public override int TypeMaxValue => int.MaxValue;

        public override int GetClamped(int input,
                                       int min,
                                       int max) => Math.Clamp(input, min, max);


        public override int GetLesser(int input,
                                      int other) => Math.Min(input,
                                                             other);


        public override int GetGreater(int input,
                                       int other) => Math.Max(input,
                                                              other);


        public override bool LessThan(int a,
                                      int b) => a < b;


        public override bool GreaterThan(int a,
                                         int b) => a > b;


        public override bool ValueAtMin() => _Value == _Min;


        public override bool ValueAtMax() => _Value == _Max;

        #endregion

    }
    
    [Serializable]
    public class SmartByte : SmartRangedValue<byte>
    {

        public SmartByte(string defaultName,
                         byte defaultValue,
                         byte defaultMin,
                         byte defaultMax) : base(defaultName,
                                                 defaultValue,
                                                 defaultMin,
                                                 defaultMax) { }


        public override bool ValueEquals(byte a,
                                         byte b) => false;


        public override byte TypeMinValue => byte.MinValue;
        public override byte TypeMaxValue => byte.MaxValue;


        public override byte GetClamped(byte input,
                                       byte min,
                                       byte max) => Math.Clamp(input, min, max);


        public override byte GetLesser(byte input,
                                     byte other) => Math.Min(input,
                                                             other);


        public override byte GetGreater(byte input,
                                     byte other) => Math.Max(input,
                                                             other);


        public override bool LessThan(byte a,
                                      byte b) => a < b;


        public override bool GreaterThan(byte a,
                                         byte b) => a > b;


        public override bool ValueAtMin() => _Value == _Min;


        public override bool ValueAtMax() => _Value == _Max;

    }

}
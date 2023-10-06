#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

using UnityEngine;

namespace Vanilla.DeltaValues
{

    [Serializable]
    public abstract class DeltaRangedValue<T> : DeltaStruct<T> where T : struct
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
                value = GetClamped(input: value,
                                   min: TypeMinValue,
                                   max: _Max);

                _Min = value;

                // if Min has risen above Value, bring Value up with it

                Value = GetGreater(input: Value,
                                   other: _Min);
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

                value = GetClamped(input: value,
                                   min: _Min,
                                   max: TypeMaxValue);

                _Max = value;

                // if Max has dropped below Value, bring Value down with it

                Value = GetLesser(input: Value,
                                  other: _Max);
            }
        }


        public override T Value
        {
            get => _Value;
            set
            {
                // First of all, make sure the incoming value is within the allowed range between Min and Max.
                // This allows us to accurately defend against false change attempts at these upper and lower bounds.
                value = GetClamped(input: value,
                                   min: Min,
                                   max: Max);

                // Now we can compare for change similarity.
                // This step works best thanks to the above clamp!
                if (ValueEquals(a: _Value,
                                b: value)) return;

                // There has been a significant change!

                // Cache the outgoing value
                var old = _Value;

                _Value = value;

                #if debug
                Debug.Log(message: $"[{Name}] changed from [{old}] to [{value}]");
                #endif

                // If the outgoing value is smaller than the incoming value...
                if (LessThan(a: old,
                             b: _Value))
                {
                    // It was an increase!

                    AtMin.Value = ValueAtMin(); // Set AtMin

                    OnValueChanged?.Invoke(arg1: _Value,
                                           arg2: old);

                    onIncrease?.Invoke(arg1: _Value,
                                       arg2: old);

                    AtMax.Value = ValueAtMax(); // Set AtMax
                }
                else
                {
                    // It was an decrease!

                    AtMax.Value = ValueAtMax(); // Set AtMax

                    OnValueChanged?.Invoke(arg1: _Value,
                                           arg2: old);

                    onDecrease?.Invoke(arg1: _Value,
                                       arg2: old);

                    AtMin.Value = ValueAtMin(); // Set AtMin
                }
            }
        }

        [SerializeField]
        public DeltaBool AtMin = new(name: "AtMin",
                                     defaultValue: false);

        [SerializeField]
        public DeltaBool AtMax = new(name: "AtMax",
                                     defaultValue: false);

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


        private void ValidateMin() => _Min = GetClamped(input: _Min,
                                                        min: TypeMinValue,
                                                        max: _Max);


        private void ValidateMax() => _Max = GetClamped(input: _Max,
                                                        min: _Min,
                                                        max: TypeMaxValue);


        private void ValidateValue() => _Value = GetClamped(input: _Value,
                                                            min: _Min,
                                                            max: _Max);


        protected void ValidateAtMin() => AtMin._Value = ValueAtMin();

        protected void ValidateAtMax() => AtMax._Value = ValueAtMax();



        #endregion

        #region Construction



        public DeltaRangedValue() { }

        public DeltaRangedValue(string name) : base(name: name) { }


        public DeltaRangedValue(string defaultName,
                                T defaultValue) : base(name: defaultName,
                                                       defaultValue: defaultValue) { }


        public DeltaRangedValue(string defaultName,
                                T defaultValue,
                                T defaultMin,
                                T defaultMax) : base(name: defaultName,
                                                     defaultValue: defaultValue)
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



        public abstract T TypeMinValue
        {
            get;
        }

        public abstract T TypeMaxValue
        {
            get;
        }


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


        // Thanks to the nature of floating point inprecision,
        // they handle AtMin and AtMax state completely differently (separate epsilon).

        public abstract bool ValueAtMin();

        public abstract bool ValueAtMax();



        #endregion

    }

}
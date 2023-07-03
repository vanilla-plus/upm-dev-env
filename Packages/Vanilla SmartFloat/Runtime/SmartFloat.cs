//#if DEVELOPMENT_BUILD
#define debug
//#endif

using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla
{

    [Serializable]
    public class SmartFloat : ISerializationCallbackReceiver
    {

        [SerializeField]
        public SmartBool AtMin = new SmartBool(true);

        [SerializeField]
        protected float _Min = 0.0f;
        public float Min
        {
            get => _Min;
            set
            {
                value = Mathf.Clamp(value: value,
                                    min: float.MinValue,
                                    max: _Max);
                
                if (Mathf.Abs(_Min - value) < MinMaxEpsilon) return;

                _Min = value;

                if (Value < _Min)
                {
                    Value = _Min;
                }
            }
        }
        
        [SerializeField]
        protected float _value = 0.0f;
        public float Value
        {
            get => _value;
            set
            {
                // Clamp the new value between min/max
                
                value = Mathf.Clamp(value: value, min: _Min, max: _Max);

                // Compare with the current cached value - stop here if they're already the same
                
                if (Mathf.Abs(f: _value - value) < ChangeEpsilon) return;

                var outgoing = _value;

                _value = value;

                // Is the new value higher than the old value?
                // We check this to make sure we process event invocations in the correct order
                
                if (outgoing < value)
                {
                    AtMin.Value = ValueIsAtMin;

                    onChange?.Invoke(_value);

                    onIncrease?.Invoke(_value);

                    AtMax.Value = ValueIsAtMax;
                }
                else
                {
                    AtMax.Value = ValueIsAtMax;

                    onChange?.Invoke(_value);

                    onDecrease?.Invoke(_value);

                    AtMin.Value = ValueIsAtMin;
                }
            }
        }
        
        
        [SerializeField]
        protected float _Max = 1.0f;
        public float Max
        {
            get => _Max;
            set
            {
                value = Mathf.Clamp(value: value,
                                    min: _Min,
                                    max: float.MaxValue);
                
                if (Mathf.Abs(_Max - value) < MinMaxEpsilon) return;

                _Max = value;

                if (Value > _Max)
                {
                    Value = _Max;
                }
            }
        }

        [SerializeField]
        public SmartBool AtMax = new SmartBool(false);


        private Action<float> onChange;
        public Action<float> OnChange
        {
            get => onChange;
            set => onChange = value;
        }

        private Action<float> onIncrease;
        public Action<float> OnIncrease
        {
            get => onIncrease;
            set => onIncrease = value;
        }

        private Action<float> onDecrease;
        public Action<float> OnDecrease
        {
            get => onDecrease;
            set => onDecrease = value;
        }

        [SerializeField]
        public float ChangeEpsilon = Mathf.Epsilon;
        
        [SerializeField]
        public float MinMaxEpsilon = Mathf.Epsilon;

        public SmartFloat(float startingValue) => Value = startingValue;


        public virtual void OnValidate()
        {
            // This runs on Update while the object is selected :|
            // Be careful performance-wise...

            MinMaxEpsilon = Mathf.Clamp(value: MinMaxEpsilon,
                                  min: Mathf.Epsilon,
                                  max: float.MaxValue);
            
            _Min = Mathf.Clamp(value: _Min,
                               min: float.MinValue,
                               max: _Max);

            _Max = Mathf.Clamp(value: _Max,
                               min: _Min,
                               max: float.MaxValue);

            _value = Mathf.Clamp(value: _value,
                                 min: _Min,
                                 max: _Max);

            AtMin.Value = ValueIsAtMin;
            AtMax.Value = ValueIsAtMax;
        }

        public float Normal => (_value - _Min) / (_Max - _Min);

        private bool ValueIsAtMin => Mathf.Abs(Value - _Min) < MinMaxEpsilon;
        private bool ValueIsAtMax => Mathf.Abs(Value - _Max) < MinMaxEpsilon;
        

        public bool AtMinOrMax() => AtMin.Value || AtMax.Value;


        /// <summary>
        ///     This UniTask will 'fill' the normal, frame by frame, until it is full or the passed in toggle
        ///     evaluates false.
        ///
        ///     If you need the drain to continue while the toggle is false, pass in 'false' as the second parameter.
        /// </summary>
        public async UniTask Fill(float amountPerSecond = 1.0f,
                                  float secondsToTake = 1.0f)
        {
            var rate = amountPerSecond / secondsToTake;

            while (!ValueIsAtMax)
            {
                Value += Time.deltaTime * rate;

                await UniTask.Yield();
            }
        }


        public async UniTask Fill(SmartBool conditional,
                                  bool targetCondition = true,
                                  float amountPerSecond = 1.0f, 
                                  float secondsToTake = 1.0f)
        {
            var rate = amountPerSecond / secondsToTake;

            while ((targetCondition ?
                        conditional.Value :
                        !conditional.Value)
                && !ValueIsAtMax)
            {
                Value += Time.deltaTime * rate;

                await UniTask.Yield();
            }
        }


        /// <summary>
        ///     This UniTask will 'drain' the normal, frame by frame, until it is empty or the passed in toggle
        ///     evaluates false.
        ///
        ///     If you need the drain to continue while the toggle is false, pass in false as the second parameter.
        /// </summary>
        public async UniTask Drain(float amountPerSecond = 1.0f,
                                   float secondsToTake = 1.0f)
        {
            var rate = amountPerSecond / secondsToTake;

            while (!ValueIsAtMin)
            {
                Value -= Time.deltaTime * rate;

                await UniTask.Yield();
            }
        }


        public async UniTask Drain(SmartBool conditional,
                                   bool targetCondition = true,
                                   float amountPerSecond = 1.0f,
                                   float secondsToTake = 1.0f)
        {
            var rate = amountPerSecond / secondsToTake;

            while ((targetCondition ?
                        conditional.Value :
                        !conditional.Value)
                && !ValueIsAtMin)
            {
                Value -= Time.deltaTime * rate;

                await UniTask.Yield();
            }
        }


        public void SilentSet(float value) => _value = value;

        public void OnBeforeSerialize() => OnValidate();

        public void OnAfterDeserialize() { }

    }

}
//#define debug

#if VanillaDanger
#define danger

using static Vanilla.Danger.Danger;
#endif

using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla
{

    [Serializable]
    public class SmartFloat
    {

        [SerializeField]
        private float _Min = 0.0f;
        public float Min
        {
            get => _Min;
            set
            {
                value = Mathf.Clamp(value: value,
                                    min: float.MinValue,
                                    max: _Max);
                
                if (Mathf.Abs(_Min - value) < Mathf.Epsilon) return;

                _Min = value;

                if (Value < _Min)
                {
                    Value = _Min;
                }
            }
        }
        
        [SerializeField]
        private float _Max = 1.0f;
        public float Max
        {
            get => _Max;
            set
            {
                value = Mathf.Clamp(value: value,
                                    min: _Min,
                                    max: float.MaxValue);
                
                if (Mathf.Abs(_Max - value) < Mathf.Epsilon) return;

                _Max = value;

                if (Value > _Max)
                {
                    Value = _Max;
                }
            }
        }

//        [SerializeField]
//        private Toggle _AtMin = new Toggle(true);
        public SmartBool AtMin => new SmartBool(true);

//        [SerializeField]
//        private Toggle _AtMax = new Toggle(false);
        public SmartBool AtMax => new SmartBool(false);

       [Range(min: 0,
               max: 1)]
        [SerializeField]
        private float _value = 0.0f;
        public float Value
        {
            get => _value;
            set
            {
                // Clamp the new value between min/max
                
                value = Mathf.Clamp(value: value,
                                    min: _Min,
                                    max: _Max);

                // Compare with the current cached value - stop here if they're already the same
                
                #if danger
                if (BitwiseEquals(a: _value, b: value)) return;
                #else
                if (Mathf.Abs(f: _value - value) < Mathf.Epsilon) return;
                #endif

                // The new value is different!
                // outgoing = old
                // _value/value = new
                
                var outgoing = _value;

                _value = value;

                // Is the new value higher than the old value?
                // We check this to make sure we process event invocations in the correct order
                
                if (outgoing < value)
                {
                    #if danger
                    if (BitwiseEquals(a: outgoing, b: _Min))
                    #else
                    if (Mathf.Abs(f: outgoing - _Min) < Mathf.Epsilon)
                    #endif
                    {
                        AtMin.Value = false;
                    }
                    
                    onChange?.Invoke(_value);

                    onIncrease?.Invoke(_value);

                    #if danger
                    if (BitwiseEquals(a: _value, b: _Max))
                    #else
                    if (Mathf.Abs(f: outgoing - _Max) < Mathf.Epsilon)
                    #endif
                    {
                        AtMax.Value = true;
                    }
                }
                else
                {
                    #if danger
                    if (BitwiseEquals(a: outgoing, b: _Max))
                    #else
                    if (Mathf.Abs(f: outgoing - _Max) < Mathf.Epsilon)
                    #endif
                    {
                        AtMax.Value = false;
                    }

                    onChange?.Invoke(_value);

                    onDecrease?.Invoke(_value);

                    #if danger
                    if (BitwiseEquals(a: _value, b: _Min))
                    #else
                    if (Mathf.Abs(f: outgoing - _Min) < Mathf.Epsilon)
                    #endif
                    {
                        AtMin.Value = true;
                    }
                }
            }
        }

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


        public SmartFloat(float startingValue) => Value = startingValue;


        public void OnValidate()
        {
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


        private bool ValueIsAtMin => Mathf.Abs(Value - _Min) < Mathf.Epsilon;
        private bool ValueIsAtMax => Mathf.Abs(Value - _Max) < Mathf.Epsilon;
        

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

    }

}
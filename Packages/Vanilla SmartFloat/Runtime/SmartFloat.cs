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
    public class SmartFloat : ISerializationCallbackReceiver
    {

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
                
                if (Mathf.Abs(_Min - value) < Epsilon) return;

                _Min = value;

                if (Value < _Min)
                {
                    Value = _Min;
                }
            }
        }
        
//       [Range(min: 0,
//               max: 1)]
        [SerializeField]
        protected float _value = 0.0f;
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
                if (Mathf.Abs(f: _value - value) < Epsilon) return;
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
                    if (Mathf.Abs(f: outgoing - _Min) < Epsilon)
                    #endif
                    {
                        AtMin.Value = false;
                    }
                    
                    #if UNITY_EDITOR
                        if (Application.isPlaying) // We don't want to invoke these Actions if in the Editor and outside play mode
                        {
                            onChange?.Invoke(_value);

                            onIncrease?.Invoke(_value);
                        }
                    #else
                        onChange?.Invoke(_value);

                        onIncrease?.Invoke(_value);
                    #endif

                    #if danger
                    if (BitwiseEquals(a: _value, b: _Max))
                    #else
                    if (Mathf.Abs(f: outgoing - _Max) < Epsilon)
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
                        if (Mathf.Abs(f: outgoing - _Max) < Epsilon)
                    #endif
                    {
                        AtMax.Value = false;
                    }

                    #if UNITY_EDITOR
                        if (Application.isPlaying) // We don't want to invoke these Actions if in the Editor and outside play mode
                        {
                            onChange?.Invoke(_value);

                            onDecrease?.Invoke(_value);
                        }
                    #else
                        onChange?.Invoke(_value);

                        onDecrease?.Invoke(_value);
                    #endif

                    #if danger
                        if (BitwiseEquals(a: _value, b: _Min))
                    #else
                        if (Mathf.Abs(f: outgoing - _Min) < Epsilon)
                    #endif
                    {
                        AtMin.Value = true;
                    }
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
                
                if (Mathf.Abs(_Max - value) < Epsilon) return;

                _Max = value;

                if (Value > _Max)
                {
                    Value = _Max;
                }
            }
        }

//        private Toggle _AtMin = new Toggle(true);
//        [SerializeField]

//        private Toggle _AtMax = new Toggle(false);
//        [SerializeField]
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
        public float Epsilon = Mathf.Epsilon;

        public SmartFloat(float startingValue) => Value = startingValue;


        public virtual void OnValidate()
        {
            // This runs on Update while the object is selected :|
            // Be careful performance-wise...

            Epsilon = Mathf.Clamp(value: Epsilon,
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

        private bool ValueIsAtMin => Mathf.Abs(Value - _Min) < Epsilon;
        private bool ValueIsAtMax => Mathf.Abs(Value - _Max) < Epsilon;
        

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
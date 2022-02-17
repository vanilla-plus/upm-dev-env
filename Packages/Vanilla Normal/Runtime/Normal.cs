using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla
{

    [Serializable]
    public class Normal : INormal
    {

        [SerializeField]
        private Toggle empty = new(startingState: true);
        public Toggle Empty => empty;

        [SerializeField]
        private Toggle full = new(startingState: false);
        public Toggle Full => full;

        [Range(min: 0,
               max: 1)]
        [SerializeField]
        private float _value = 0.0f;
        public float Value
        {
            get => _value;
            set
            {
                value = Mathf.Clamp01(value: value);

                if (Mathf.Abs(f: _value - value) < Mathf.Epsilon) return;

                var outgoing = _value;

                _value = value;

                if (outgoing < value)
                {
                    if (outgoing < Mathf.Epsilon)
                    {
                        empty.State = false;
                    }

                    onChange?.Invoke(obj: _value);

                    onIncrease?.Invoke(obj: _value);

                    if (1.0f - _value < Mathf.Epsilon)
                    {
                        full.State = true;
                    }
                }
                else
                {
                    if (1.0f - outgoing < Mathf.Epsilon)
                    {
                        full.State = false;
                    }

                    onChange?.Invoke(obj: _value);

                    onDecrease?.Invoke(obj: _value);

                    if (_value < Mathf.Epsilon)
                    {
                        empty.State = true;
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


        public Normal(float startingValue) => Value = startingValue;


        public void OnValidate()
        {
            _value = Mathf.Clamp01(value: _value);

            empty.State = Value        < Mathf.Epsilon;
            full.State  = 1.0f - Value < Mathf.Epsilon;
        }


        /// <summary>
        ///     This UniTask will 'fill' the normal, frame by frame, until it is full or the passed in toggle
        ///     evaluates false.
        ///
        ///     If you need the drain to continue while the toggle is false, pass in false as the second parameter.
        /// </summary>
        public async UniTask Fill(Toggle conditional,
                                  bool targetCondition = true,
                                  float speed = 1.0f)
        {
            if (ReferenceEquals(objA: conditional,
                                objB: null))
            {
                while (Value < 1.0f)
                {
                    Value += Time.deltaTime * speed;

                    await UniTask.Yield();
                }
            }
            else
            {
                while ((targetCondition ? conditional.State : !conditional.State) &&
                       Value < 1.0f)
                {
                    Value += Time.deltaTime * speed;

                    await UniTask.Yield();
                }
            }
        }


        /// <summary>
        ///     This UniTask will 'drain' the normal, frame by frame, until it is empty or the passed in toggle
        ///     evaluates false.
        ///
        ///     If you need the drain to continue while the toggle is false, pass in false as the second parameter.
        /// </summary>
        public async UniTask Drain(Toggle conditional,
                                   bool targetCondition = true,
                                   float speed = 1.0f)
        {
            if (ReferenceEquals(objA: conditional,
                                objB: null))
            {
                while (Value > 0.0f)
                {
                    Value -= Time.deltaTime * speed;

                    await UniTask.Yield();
                }
            }
            else
            {
                while ((targetCondition ? conditional.State : !conditional.State) &&
                       Value > 0.0f)
                {
                    Value -= Time.deltaTime * speed;

                    await UniTask.Yield();
                }
            }

        }

    }

}
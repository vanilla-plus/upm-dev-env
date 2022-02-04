using System;
using System.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MenuMachine
{

    // Why 2 different normals?
    
    // This one is access-tight; it can only be driven from the outside by the public toggle or the FillInstant / DrainInstant calls.
    // It is very tightly verbosely controlled as a result.

    [Serializable]
    public class Normal
    {

        // Toggle is a regular C# class, not a MonoBehaviour.
        // Because of this, it can be assigned another Toggle already in memory.
        // i.e. normal.toggle = aLightSwitch.toggle;
        // Likewise, it will error if you do access this one directly without creating it first using 'new'.
        
        [SerializeField]
        public  Toggle toggle;

        [SerializeField]
        private bool _isEmpty = true;
        public bool IsEmpty => _isEmpty;

        [SerializeField]
        private bool _isFull = false;
        public bool IsFull => _isFull;

        [SerializeField]
        internal float _value = 0.0f;
        public float Value
        {
            get => _value;
            private set => _value = Mathf.Clamp01(value: value);
        }

        [Range(min: 0.01f, max: 10.0f)]
        [SerializeField]
        public float fillRate = 2.0f;

        [Range(min: 0.01f, max: 10.0f)]
        [SerializeField]
        public float drainRate = 2.0f;

        public Action onBecameFull;
        public Action onBecameEmpty;

        public Action onNoLongerFull;
        public Action onNoLongerEmpty;

        public Action<float> onFillFrame;
        public Action<float> onDrainFrame;
        
        public Normal(Toggle activator)
        {
            toggle = activator ?? new Toggle();

            toggle.onChange += a =>
                               {
                                   if (a) 
                                       Fill();
                                   else 
                                       Drain();
                               };
        }


        private async Task Fill()
        {
            if (_value < Mathf.Epsilon)
            {
                _isEmpty        = false;

                onNoLongerEmpty?.Invoke();
            }

            while (toggle.active &&
                   _value < 1.0f)
            {
                Value += Time.deltaTime * fillRate;

                onFillFrame?.Invoke(_value);

                await Task.Yield();
            }

            if (1.0f - _value < Mathf.Epsilon)
            {
                _isFull = true;

                onBecameFull?.Invoke();
            }
        }


        private async Task Drain()
        {
            if (1.0f - _value < Mathf.Epsilon)
            {
                _isFull = false;

                onNoLongerFull?.Invoke();
            }

            while (!toggle.active &&
                   _value > 0.0f)
            {
                Value -= Time.deltaTime * drainRate;

                onDrainFrame?.Invoke(_value);

                await Task.Yield();
            }

            if (_value < Mathf.Epsilon)
            {
                _isEmpty = true;

                onBecameEmpty?.Invoke();
            }
        }


        public void FillInstant()
        {
            if (_isFull) return;
            
            if (!toggle._active)
            {
                toggle._active = true;

                toggle.onChange?.Invoke(obj: true);
            }

            if (_isEmpty)
            {
                _isEmpty = false;
                
                onNoLongerEmpty?.Invoke();
            }
            
            Value = 1.0f;

            onFillFrame?.Invoke(1.0f);
            
            _isFull = true;
            
            onBecameFull?.Invoke();
        }


        public void DrainInstant()
        {
            if (_isEmpty) return;

            if (toggle._active)
            {
                toggle._active = false;

                toggle.onChange?.Invoke(obj: false);
            }

            if (_isFull)
            {
                _isFull = false;
                
                onNoLongerFull?.Invoke();
            }
            
            Value = 0.0f;
            
            onDrainFrame?.Invoke(0.0f);

            _isEmpty = true;
            
            onBecameEmpty?.Invoke();
        }

    }

}
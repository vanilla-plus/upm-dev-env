using System;
using System.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MenuMachine
{

    [Serializable]
    public class Normal2
    {

        // Toggle is a regular C# class, not a MonoBehaviour.
        // Because of this, it can be assigned another Toggle already in memory.
        // i.e. normal.toggle = aLightSwitch.toggle;
        // Likewise, it will error if you do access this one directly without creating it first using 'new'.
        
        [SerializeField]
        public  Toggle toggle;

        [SerializeField]
        public Toggle empty = new();
        
        [SerializeField]
        public Toggle full  = new();

        [SerializeField]
        internal float _value;
        public float Value
        {
            get => _value;
            set
            {
//                var newValue = Mathf.Clamp01(value: value);

//                if (Mathf.Abs(f: newValue - _value) < Mathf.Epsilon) return;

//                _value = newValue;

                _value = Mathf.Clamp01(value: value);

                onChange?.Invoke(obj: _value);
            }
        }

        [Range(min: 0.01f, max: 10.0f)]
        [SerializeField]
        public float fillRate = 2.0f;

        [Range(min: 0.01f, max: 10.0f)]
        [SerializeField]
        public float drainRate = 2.0f;

        public Action<float> onChange;
        
        public Action onBecameFull;
        public Action onBecameEmpty;

        public Action onNoLongerFull;
        public Action onNoLongerEmpty;

        public Action<float> onFillFrame;
        public Action<float> onDrainFrame;
        
//        public Action        onFillStart;
//        public Action<float> onFillFrame;
//        public Action        onFillComplete;

//        public Action        onDrainStart;
//        public Action<float> onDrainFrame;
//        public Action        onDrainComplete;


        public Normal2() => toggle.onChange += a =>
                                               {
                                                   if (a) Fill();
                                                   else Drain();
                                               };


        private async Task Fill()
        {
            if (_value < Mathf.Epsilon)
            {
                empty.active = false;

//                onFillStart?.Invoke();
            }

            while (toggle.active &&
                   _value < 1.0f)
            {
                Value += Time.deltaTime * fillRate;

//                onFillFrame?.Invoke(_value);

                await Task.Yield();
            }

            if (1.0f - _value < Mathf.Epsilon)
            {
                full.active = true;

//                onFillComplete?.Invoke();
            }
        }


        private async Task Drain()
        {
            UpdateFull();

            while (!toggle.active &&
                   _value > 0.0f)
            {
                Value -= Time.deltaTime * drainRate;

//                onDrainFrame?.Invoke(_value);

                await Task.Yield();
            }

            UpdateEmpty();
        }


        public void FillInstant()
        {
            if (full._active) return;
            
            if (!toggle._active)
            {
                toggle._active = true;

                toggle.onChange?.Invoke(obj: true);
            }

            if (empty._active)
            {
                empty._active = false;
                
                empty.onChange?.Invoke(false);
            }
            
            Value = 1.0f;
            
            full._active = true;
            
            full.onChange?.Invoke(obj: true);
        }


        public void DrainInstant()
        {
            if (empty._active) return;

            if (toggle._active)
            {
                toggle._active = false;

                toggle.onChange?.Invoke(obj: false);
            }

            if (full._active)
            {
                full._active = false;
                
                full.onChange?.Invoke(obj: false);
            }
            
            Value = 0.0f;

            empty._active = true;
            
            empty.onChange?.Invoke(obj: true);
        }

        private void UpdateEmpty() => empty.active = _value       < Mathf.Epsilon;
        private void UpdateFull()  => full.active = 1.0f - _value < Mathf.Epsilon;

    }

}
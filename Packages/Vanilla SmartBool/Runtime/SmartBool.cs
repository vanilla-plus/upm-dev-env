#if DEBUG && SMARTBOOL
#define debug
#endif

using System;

using UnityEngine;

namespace Vanilla
{

    [Serializable]
    public class SmartBool : ISerializationCallbackReceiver
    {

        [SerializeField]
        public string name = "Unknown SmartBool";
        
        [SerializeField]
        public bool _value;
        public bool Value
        {
            get => _value;
            set
            {
                if (_value == value) return;

                #if debug
                Debug.Log($"[{name}] changed from [{_value}] to [{value}]");
                #endif
                
                _value = value;

                onChange?.Invoke(_value);

                if (_value)
                {
                    onTrue?.Invoke();
                }
                else
                {
                    onFalse?.Invoke();
                }
            }
        }

        public Action<bool> onChange;

        public Action onTrue;
        public Action onFalse;

        public SmartBool(bool startingValue) => _value = startingValue;


        public static implicit operator bool(SmartBool smartBool) => smartBool is
                                                               {
                                                                   Value: true
                                                               };
        
        public void Flip() => Value = !_value;


        public void Invoke()
        {
            _value = !_value;

            Value = !_value;
        }

        public void SilentSet(bool state) => _value = state;

        public void OnValidate() { }

        public void OnBeforeSerialize() => OnValidate();

        public void OnAfterDeserialize() { }

    }

}
#if DEBUG && TOGGLE
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
        private bool _value;
        public bool Value
        {
            get => _value;
            set
            {
                if (_value == value) return;

                #if debug
                Debug.Log($"Toggle state changed from [{_value}] to [{value}]");
                #endif
                
                _value = value;

                #if UNITY_EDITOR
                    if (Application.isPlaying) // We don't want to invoke this Action if in the Editor and outside play mode
                    {
                        onChange?.Invoke(_value);
                    }
                #else
                    onChange?.Invoke(_value);
                #endif

                if (_value)
                {
                    #if UNITY_EDITOR
                        if (Application.isPlaying) // We don't want to invoke this Action if in the Editor and outside play mode
                        {
                            onTrue?.Invoke();
                        }
                    #else
                        onTrue?.Invoke(_value);
                    #endif
                }
                else
                {
                    #if UNITY_EDITOR
                        if (Application.isPlaying) // We don't want to invoke this Action if in the Editor and outside play mode
                        {
                            onFalse?.Invoke();
                        }
                    #else
                        onFalse?.Invoke(_value);
                    #endif
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

        public void SilentSet(bool state) => _value = state;

        public void OnValidate() { }

        public void OnBeforeSerialize() => OnValidate();

        public void OnAfterDeserialize() { }

    }

}
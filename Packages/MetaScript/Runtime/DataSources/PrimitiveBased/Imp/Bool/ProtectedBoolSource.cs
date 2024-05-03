using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{

    [Serializable]
    public class ProtectedBoolSource : BoolSource,
                                       IProtectedSource<bool>
    {

        [SerializeField]
        private string _name = "Unnamed ProtectedBoolSource";
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        [SerializeField]
        private bool _value = false;
        public sealed override bool Value
        {
            get => _value;
            set
            {
                if (_value == value) return;

                var outgoing = _value;
                
                _value = value;
                
//                #if debug
//                Debug.Log($"[{Time.frameCount}] [{Name}] was changed from [{outgoing}] to [{value}]");
//                #endif
                
                if (_value)
                {
                    OnTrue?.Invoke();
                }
                else
                {
                    OnFalse?.Invoke();
                }

                OnSet?.Invoke(_value);
                OnSetWithHistory?.Invoke(_value,
                                         outgoing);
            }
        }

        public ProtectedBoolSource() { }
        public ProtectedBoolSource(bool defaultValue) => Value = defaultValue;
//
//        public ProtectedBoolSource(string name,
//                                   bool defaultValue) : base(name: name) => Value = defaultValue;
        
        public override void OnBeforeSerialize() { }

        public override void OnAfterDeserialize() { }
        
        public override string ToString() => Value.ToString();

    }

}
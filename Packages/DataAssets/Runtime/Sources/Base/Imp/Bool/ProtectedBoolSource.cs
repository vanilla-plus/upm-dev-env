using System;

using UnityEngine;

namespace Vanilla.DataSources
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
                
                #if debug
                Debug.Log($"[{Name}] was changed from [{outgoing}] to [{value}]");
                #endif

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
        
    }

}
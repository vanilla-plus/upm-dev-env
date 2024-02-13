using System;

using UnityEngine;

namespace Vanilla.DataSources
{

    [Serializable]
    public class ProtectedBoolSource : BoolSource,
                                       IProtectedSource<bool>
    {

        [SerializeField]
        private bool _value = false;
        public sealed override bool Value
        {
            get => _value;
            set
            {
                if (_value == value) return;

                _value = value;

                OnSet?.Invoke(_value);

                OnSetWithHistory?.Invoke(_value,
                                         !_value);
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
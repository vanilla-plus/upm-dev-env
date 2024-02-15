using System;

using UnityEngine;

namespace Vanilla.DataSources
{
    
    [Serializable]
    public class DirectBoolSource : BoolSource
    {

        [SerializeField]
        private bool _value = false;
        public override bool Value
        {
            get => _value;
            set
            {
                var old = _value;
                
                _value = value;
                
                OnSet?.Invoke(_value);
                OnSetWithHistory?.Invoke(_value, old);
            }
        }

        public override void OnBeforeSerialize() { }

        public override void OnAfterDeserialize() => Value = _value;

    }
}

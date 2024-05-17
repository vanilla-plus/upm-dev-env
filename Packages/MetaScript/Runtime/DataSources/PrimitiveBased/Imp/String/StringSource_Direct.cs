using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources.Strings
{
    
    [Serializable]
    public class StringSource_Direct : StringSource
    {

        [SerializeField]
        private string _value;
        public override string Value
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

        public override void OnAfterDeserialize() { }

    }
}

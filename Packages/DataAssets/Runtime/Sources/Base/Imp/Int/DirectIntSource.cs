using System;

using UnityEngine;

namespace Vanilla.DataSources
{
    
    [Serializable]
    public class DirectIntSource : IntSource
    {

        [SerializeField]
        private int _value;
        public override int Value
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

using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
    
    [Serializable]
    public class DirectFloatSource : FloatSource
    {

        [SerializeField]
        private float _value = 0.0f;
        public override float Value
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

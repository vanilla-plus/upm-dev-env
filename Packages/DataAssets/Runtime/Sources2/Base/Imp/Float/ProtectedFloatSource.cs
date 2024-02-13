using System;

using UnityEngine;

namespace Vanilla.DataSources
{
    

    [Serializable]
    public class ProtectedFloatSource : FloatSource, 
                                        IProtectedSource<float>
    {

        [SerializeField]
        public float _changeEpsilon = Mathf.Epsilon;
        public float ChangeEpsilon
        {
            get => _changeEpsilon;
            set => _changeEpsilon = value;
        }

        [SerializeField]
        private float _value = 0.0f;
        public override float Value
        {
            get => _value;
            set
            {
                if (Mathf.Abs(_value - value) > ChangeEpsilon) return;
                
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

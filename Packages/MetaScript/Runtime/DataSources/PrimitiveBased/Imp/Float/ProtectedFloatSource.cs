using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
    

    [Serializable]
    public class ProtectedFloatSource : FloatSource, 
                                        IProtectedSource<float>
    {
        
        [SerializeField]
        private string _name = "Unnamed ProtectedFloatSource";
        public string Name
        {
            get => _name;
            set => _name = value;
        }





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
                
                var outgoing = _value;

                _value = value;

                #if debug
                Debug.Log($"[{Name}] was changed from [{outgoing}] to [{value}]");
                #endif
                
                OnSet?.Invoke(value);
                OnSetWithHistory?.Invoke(value, outgoing);
            }
        }

        public override void OnBeforeSerialize() { }

        public override void OnAfterDeserialize() { }

    }
}

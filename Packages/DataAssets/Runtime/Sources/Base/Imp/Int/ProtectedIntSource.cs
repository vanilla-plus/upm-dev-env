using System;

using UnityEngine;

namespace Vanilla.DataSources
{
    

    [Serializable]
    public class ProtectedIntSource : IntSource, 
                                      IProtectedSource<int>
    {

        [SerializeField]
        private string _name = "Unnamed ProtectedIntSource";
        public string Name
        {
            get => _name;
            set => _name = value;
        }



        [SerializeField]
        private int _value;
        public override int Value
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
                
                OnSet?.Invoke(value);
                OnSetWithHistory?.Invoke(value, outgoing);
            }
        }

        public override void OnBeforeSerialize() { }

        public override void OnAfterDeserialize() { }

    }
}

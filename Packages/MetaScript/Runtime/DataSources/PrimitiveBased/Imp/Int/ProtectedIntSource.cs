using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
    

    [Serializable]
    public class ProtectedIntSource : IntSource, 
                                      IProtectedSource<int>
    {

        [SerializeField]
        private string _Name = "Unnamed ProtectedIntSource";
        public string Name
        {
            get => _Name;
            set => _Name = value;
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
                
                OnSet?.Invoke(value);
                OnSetWithHistory?.Invoke(value, outgoing);
            }
        }

        public override void OnBeforeSerialize() { }

        public override void OnAfterDeserialize() { }

    }
}

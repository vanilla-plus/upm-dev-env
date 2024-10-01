using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
    
    [Serializable]
    public class FloatSource_Direct : FloatSource, IGettableSource<float>, ISettableSource<float>, IObservableSource<float>
    {

        [SerializeField]
        internal float _value = 0.0f;
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

        [NonSerialized]
        private Action<float> _onSet;
        public Action<float> OnSet
        {
            get => _onSet;
            set => _onSet = value;
        }

        [NonSerialized]
        private Action<float, float> _onSetWithHistory;
        public Action<float, float> OnSetWithHistory
        {
            get => _onSetWithHistory;
            set => _onSetWithHistory = value;
        }

    }
}

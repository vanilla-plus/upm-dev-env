using System;

using UnityEngine;

namespace Vanilla.DataSources
{
    
    [Serializable]
    public abstract class FloatSource : IDataSource<float>
    {

        [SerializeField]
        private string _name = "Unnamed FloatSource";
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public abstract float Value
        {
            get;
            set;
        }

        [NonSerialized]
        private Action<float> _onValueChange;
        public Action<float> OnSet
        {
            get => _onValueChange;
            set => _onValueChange = value;
        }

        [NonSerialized]
        private Action<float, float> _onSetWithHistory;
        public Action<float, float> OnSetWithHistory
        {
            get => _onSetWithHistory;
            set => _onSetWithHistory = value;
        }

        public abstract void OnBeforeSerialize();

        public abstract void OnAfterDeserialize();

    }
}

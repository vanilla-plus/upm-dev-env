using System;

using UnityEngine;

namespace Vanilla.DataSources
{
    
    [Serializable]
    public abstract class IntSource : IDataSource<int>
    {

        [SerializeField]
        private string _name = "Unnamed IntSource";
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public abstract int Value
        {
            get;
            set;
        }

        [NonSerialized]
        private Action<int> _onValueChange;
        public Action<int> OnSet
        {
            get => _onValueChange;
            set => _onValueChange = value;
        }

        [NonSerialized]
        private Action<int, int> _onSetWithHistory;
        public Action<int, int> OnSetWithHistory
        {
            get => _onSetWithHistory;
            set => _onSetWithHistory = value;
        }

        public abstract void OnBeforeSerialize();

        public abstract void OnAfterDeserialize();

    }
}

using System;

using UnityEngine;

namespace Vanilla.DataSources
{
    
    [Serializable]
    public abstract class BoolSource : IDataSource<bool>
    {

        [SerializeField]
        private string _name = "Unnamed BoolSource";
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public abstract bool Value
        {
            get;
            set;
        }
        
        [NonSerialized]
        private Action<bool> _onSet;
        public Action<bool> OnSet
        {
            get => _onSet;
            set => _onSet = value;
        }

        [NonSerialized]
        private Action<bool, bool> _onSetWithHistory;
        public Action<bool, bool> OnSetWithHistory
        {
            get => _onSetWithHistory;
            set => _onSetWithHistory = value;
        }

        public BoolSource() => _name = "Unnamed BoolSource";

        public BoolSource(string name) => _name = name;

        public abstract void OnBeforeSerialize();

        public abstract void OnAfterDeserialize();

    }
}

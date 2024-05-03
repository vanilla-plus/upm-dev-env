using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
    
    [Serializable]
    public abstract class BoolSource : IDataSource<bool>
    {

//        [SerializeField] private string _name = "Unnamed BoolSource";
//        public string Name
//        {
//            get => _name;
//            set => _name = value;
//        }

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
        
        [NonSerialized]
        private Action _onTrue;
        public Action OnTrue
        {
            get => _onTrue;
            set => _onTrue = value;
        }
        
        [NonSerialized]
        private Action _onFalse;
        public Action OnFalse
        {
            get => _onFalse;
            set => _onFalse = value;
        }
        

//        public BoolSource() => _name = "Unnamed BoolSource";
//
//        public BoolSource(string name) => _name = name;

        public void Flip() => Value = !Value;
        
        public abstract void OnBeforeSerialize();

        public abstract void OnAfterDeserialize();


        public static implicit operator bool(BoolSource input) => input is
                                                                  {
                                                                      Value: true
                                                                  };

    }
}

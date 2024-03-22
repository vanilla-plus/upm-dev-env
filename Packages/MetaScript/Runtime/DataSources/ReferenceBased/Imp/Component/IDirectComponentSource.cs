using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources.GenericComponent;

namespace Vanilla.MetaScript
{
    
    public interface IDirectComponentSource<T,S> : IComponentSource<T,S> 
        where T : Component
        where S : IComponentSource<T,S>
    {
        
//        [SerializeField]
//        private T _value;
//        public override T Value
//        {
//            get => _value;
//            set
//            {
//                var old = _value;
//                
//                _value = value;
//                
//                OnSet?.Invoke(_value);
//                OnSetWithHistory?.Invoke(_value, old);
//            }
//        }
//
//        public override void OnBeforeSerialize() { }
//
//        public override void OnAfterDeserialize() { }

    }
}

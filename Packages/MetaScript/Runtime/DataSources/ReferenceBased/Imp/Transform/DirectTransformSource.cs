using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript
{

    [Serializable]
    public class DirectTransformSource : IDirectComponentSource<Transform, DirectTransformSource>, ITransformSource
    {

        [SerializeField]
        private Transform _value;
        public Transform Value
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

        [NonSerialized]
        private Action<Transform> _onSet;
        public Action<Transform> OnSet
        {
            get => _onSet;
            set => _onSet = value;
        }

        [NonSerialized]
        private Action<Transform,Transform> _onSetWithHistory;
        public Action<Transform,Transform> OnSetWithHistory
        {
            get => _onSetWithHistory;
            set => _onSetWithHistory = value;
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize() { }

    }
}

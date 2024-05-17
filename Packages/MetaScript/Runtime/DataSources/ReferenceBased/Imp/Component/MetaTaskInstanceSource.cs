using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MetaScript.DataSources.GenericComponent
{
    
    [SerializeField]
    public class MetaTaskInstanceSource : IComponentSource<MetaTaskInstance, MetaTaskInstanceSource>
    {

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize() { }

        [SerializeField]
        private MetaTaskInstance _value;
        public MetaTaskInstance Value
        {
            get => _value;
            set => _value = value;
        }

        [SerializeField]
        private Action<MetaTaskInstance> _onSet;
        public Action<MetaTaskInstance> OnSet
        {
            get => _onSet;
            set => _onSet = value;
        }

        [SerializeField]
        private Action<MetaTaskInstance, MetaTaskInstance> _onSetWithHistory;
        public Action<MetaTaskInstance, MetaTaskInstance> OnSetWithHistory
        {
            get => _onSetWithHistory;
            set => _onSetWithHistory = value;
        }

    }
}

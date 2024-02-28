using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{

    [Serializable]
    public class DirectColorSource : ColorSource
    {

        [SerializeField]
        private Color _value;
        public override Color Value
        {
            get => _value;
            set
            {
                var old = _value;

                _value = value;

                OnSet?.Invoke(_value);

                OnSetWithHistory?.Invoke(_value,
                                         old);
            }
        }

        public override void OnBeforeSerialize() { }

        public override void OnAfterDeserialize() { }

    }

}
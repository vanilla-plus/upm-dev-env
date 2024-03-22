using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{

    [Serializable]
    public class DirectVec4Source : Vec4Source
    {

        [SerializeField]
        private Vector4 _value;
        public override Vector4 Value
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
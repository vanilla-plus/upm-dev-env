using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{

    [Serializable]
    public class DirectVec2Source : Vec2Source
    {

        [SerializeField]
        private Vector2 _value;
        public override Vector2 Value
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
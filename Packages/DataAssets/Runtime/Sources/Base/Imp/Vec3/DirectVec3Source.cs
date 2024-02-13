using System;

using UnityEngine;

using Vanilla.DataSources;

namespace Vanilla.DataAssets
{

    [Serializable]
    public class DirectVec3Source : Vec3Source
    {

        [SerializeField]
        private Vector3 _value;
        public override Vector3 Value
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
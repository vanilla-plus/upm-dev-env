using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{

    [Serializable]
    public class FloatSource_Vec3_Distance : FloatSource
    {

        [SerializeReference] public Vec3Source A;
        [SerializeReference] public Vec3Source B;

        public override float Value
        {
            get => Vector3.Distance(A.Value,
                                    B.Value);
            set { }
        }

        public override void OnBeforeSerialize() { }

        public override void OnAfterDeserialize() { }

    }

}
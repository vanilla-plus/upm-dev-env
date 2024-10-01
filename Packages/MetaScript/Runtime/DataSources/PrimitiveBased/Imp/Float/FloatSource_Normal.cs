using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
    
    [Serializable]
    public class FloatSource_Normal : FloatSource
    {

        [SerializeReference] public FloatSource Part;
        [SerializeReference] public FloatSource Whole;
        
        public override float Value
        {
            get => Part.Value / Whole.Value;
            set { }
        }

        public override void OnBeforeSerialize() { }

        public override void OnAfterDeserialize() { }

    }
}

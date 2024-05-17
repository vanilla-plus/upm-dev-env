using System;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataSources.Strings
{
    
    // We'll need different types of Concat depending on how they're joined
    [Serializable]
    public abstract class StringSource_Concat : StringSource_Bakable
    {

        [TypeMenu("red")]
        [SerializeReference]
        public StringSource[] Elements = Array.Empty<StringSource>();
        
        public override void OnBeforeSerialize() { }

        public override void OnAfterDeserialize() { }

    }
}

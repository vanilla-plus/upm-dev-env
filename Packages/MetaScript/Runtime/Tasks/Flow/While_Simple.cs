using System;

using UnityEngine;

namespace Vanilla.MetaScript.Flow
{
    
    [Serializable]
    public class While_Simple : While
    {

        [SerializeField]
        public bool Loop = true;
        
        protected override string CreateAutoName() => "Repeat the following tasks while...";


        public override bool Evaluate() => Loop;

    }
}

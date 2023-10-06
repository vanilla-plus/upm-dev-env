using System;

using UnityEngine;

using Vanilla.MetaScript.TaskSets;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class WhileSimple : While
    {

        [SerializeField]
        public bool Loop = true;
        
        protected override string CreateAutoName() => "Repeat the following tasks while...";


        public override bool Evaluate() => Loop;

    }
}

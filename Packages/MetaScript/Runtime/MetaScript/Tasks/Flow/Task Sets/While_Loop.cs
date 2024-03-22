using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Flow
{
    
    [Serializable]
    public class While_Loop : While_Base
    {

        [SerializeReference]
        [TypeMenu("red")]
        public BoolSource ContinueLoop;
        
        protected override string CreateAutoName() => "Repeat the following tasks while...";


        public override bool Evaluate() => ContinueLoop.Value;

    }
}

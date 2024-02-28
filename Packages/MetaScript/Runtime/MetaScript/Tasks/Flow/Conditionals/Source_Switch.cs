using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Flow
{
    
    [Serializable]
    public class Source_Switch : Switch
    {

        [SerializeReference]
        [TypeMenu("red")]
        public IntSource Index;
        
        protected override bool CanAutoName() => base.CanAutoName() && Index != null;
        
        protected override string CreateAutoName() => $"Run task at value of [{(Index is ProtectedIntSource p ? p.Name : Index is DirectIntSource d ? d.Value : Index.GetType().Name)}]";


        public override int Evaluate() => Index.Value;

    }
}

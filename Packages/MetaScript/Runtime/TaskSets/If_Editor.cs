using System;

using UnityEngine;

namespace Vanilla.MetaScript.TaskSets
{
	
	[Serializable]
    public class If_Editor : Switch
    {

        protected override string CreateAutoName() => "If running in UnityEditor...";


        public override int Evaluate() => Application.isEditor ?
                                              0 :
                                              1;

    }
}

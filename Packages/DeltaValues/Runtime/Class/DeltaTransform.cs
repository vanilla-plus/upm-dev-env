using System;

using UnityEngine;

namespace Vanilla.DeltaValues
{
	
	[Serializable]
    public class DeltaTransform : DeltaClass<Transform>
    {

	    public DeltaTransform(string name) : base(name) { }


        public DeltaTransform(string name,
                 Transform defaultValue) : base(name,
                                                defaultValue) { }

    }
}

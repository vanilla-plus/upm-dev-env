using System;

using UnityEngine;

namespace Vanilla.SmartValues
{
	
	[Serializable]
    public class SmartTransform : SmartClass<Transform>
    {

	    public SmartTransform(string name) : base(name) { }


        public SmartTransform(string name,
                 Transform defaultValue) : base(name,
                                                defaultValue) { }


        public override bool ValueEquals(Transform a,
                                         Transform b) => ReferenceEquals(a,b);

    }
}

using System;

using UnityEngine;

namespace Vanilla.SmartValues
{

	[Serializable]
	public class SmartGameObject : SmartClass<GameObject>
	{

		public SmartGameObject(string name) : base(name) { }


		public SmartGameObject(string name,
		                       GameObject defaultValue) : base(name,
		                                                       defaultValue) { }
		
		public override bool ValueEquals(GameObject a,
		                                 GameObject b) => ReferenceEquals(a,
		                                                                  b);

	}

}
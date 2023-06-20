//#if DEVELOPMENT_BUILD
#define debug
//#endif

using System;

using UnityEngine;

namespace Vanilla.SmartValues
{

	[Serializable]
	public class SmartBool : SmartStruct<bool>
	{

		public override bool ValueEquals(bool a,
		                                 bool b) => a == b;

		public SmartBool(string name) : base(name) { }


		public SmartBool(string name,
		         bool defaultValue) : base(name,
		                                   defaultValue) { }


		public SmartBool()
		{
			
		}

	}

}
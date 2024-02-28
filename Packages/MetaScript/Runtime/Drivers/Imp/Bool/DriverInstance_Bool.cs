using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MetaScript.Drivers.Bool
{
    
	[Serializable]
	public class DriverInstance_Bool : DriverInstance<bool>
	{

		[SerializeField]
		private Driver[] _drivers = Array.Empty<Driver>();
		public override Driver<bool>[] Drivers => _drivers;

	}
}
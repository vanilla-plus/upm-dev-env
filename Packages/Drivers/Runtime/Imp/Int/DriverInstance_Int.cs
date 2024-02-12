using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Drivers.Int
{
    
	[Serializable]
	public class DriverInstance_Int : DriverInstance<int>
	{

		[SerializeField]
		private Driver[] _drivers = Array.Empty<Driver>();
		public override Driver<int>[] Drivers => _drivers;

	}
}
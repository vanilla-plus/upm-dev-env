using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers.Int
{
    
	[Serializable]
	public class DriverInstance_Int : DriverInstance<int, IntSource, IntAsset, Module, Driver>
	{

		[SerializeField]
		private Driver[] _drivers = Array.Empty<Driver>();
		public override Driver[] Drivers => _drivers;

	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Drivers.Bool
{
    
	[Serializable]
	public abstract class Module : Module<bool>
	{

		public override void HandleValueChange(bool value) => Debug.Log(value);

	}
}
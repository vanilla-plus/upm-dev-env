using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Drivers.Int
{
    
	[Serializable]
	public abstract class Module : Module<int>
	{

		public override void HandleValueChange(int value) => Debug.Log(value);

	}
}
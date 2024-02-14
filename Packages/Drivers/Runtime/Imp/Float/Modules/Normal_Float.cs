using System;

using UnityEngine;

namespace Vanilla.Drivers.Float
{

	[Serializable]
	public class Normal_Float : Normal_Base<float>
	{

		protected override float Interpolate(float normal) => Mathf.Lerp(From,
		                                                                 To,
		                                                                 normal);

	}

}
using System;

using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.MetaScript.Drivers.Float
{

	[Serializable]
	public class Normal_Vec3_UnityEvent : Normal_Base<Vector3>
	{

		[SerializeField]
		public UnityEvent<Vector3> OnSet;


		protected override Vector3 Interpolate(float normal) => Vector3.Lerp(From,
		                                                                     To,
		                                                                     normal);


		protected override void HandleSet(float incoming) => OnSet.Invoke(Interpolate(incoming));

	}

}
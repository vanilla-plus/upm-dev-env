using System;

using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.MetaScript.Drivers.Float
{

	[Serializable]
	public class Normal_Float_UnityEvent : Normal_Base<float>
	{

		[SerializeField]
		public UnityEvent<float> OnSet;

		protected override float Interpolate(float normal) => Mathf.Lerp(From,
		                                                                 To,
		                                                                 normal);


		protected override void HandleSet(float incoming) => OnSet.Invoke(Interpolate(incoming));

	}

}
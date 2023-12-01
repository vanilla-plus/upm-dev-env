using System;

using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Modules.Vector1
{

	[Serializable]
	public class UnityEvent : Vec1Module,
	                          IEventModule<float>
	{

		[SerializeField]
		public UnityEvent<float> onValueChange = new();
		public UnityEvent<float> OnValueChange => onValueChange;

		public override void OnValidate(float value) => OnValueChange.Invoke(value);

		public override void Init(DriverSet driverSet) { }

		public override void HandleValueChange(float value) => OnValueChange.Invoke(value);

	}

}
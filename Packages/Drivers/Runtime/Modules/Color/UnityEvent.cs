using System;

using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Modules.Color
{

	[Serializable]
	public class UnityEvent : ColorModule,
	                          IEventModule<UnityEngine.Color>
	{

		[SerializeField]
		public UnityEvent<UnityEngine.Color> onValueChange = new();
		public UnityEvent<UnityEngine.Color> OnValueChange => onValueChange;

		public override void OnValidate(UnityEngine.Color value) => OnValueChange.Invoke(value);

		public override void Init(DriverSet driverSet) { }

		public override void HandleValueChange(UnityEngine.Color value) => OnValueChange.Invoke(value);

	}

}
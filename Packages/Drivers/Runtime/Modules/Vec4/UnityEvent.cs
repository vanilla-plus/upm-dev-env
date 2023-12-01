using System;

using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Modules.Vector4
{

	[Serializable]
	public class UnityEvent : Vec4Module,
	                          IEventModule<UnityEngine.Vector4>
	{

		[SerializeField]
		public UnityEvent<UnityEngine.Vector4> onValueChange = new();
		public UnityEvent<UnityEngine.Vector4> OnValueChange => onValueChange;

		public override void OnValidate(UnityEngine.Vector4 value) => OnValueChange.Invoke(value);

		public override void Init(DriverSet driverSet) { }

		public override void HandleValueChange(UnityEngine.Vector4 value) => OnValueChange.Invoke(value);

	}

}
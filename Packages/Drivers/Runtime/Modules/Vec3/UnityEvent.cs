using System;

using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Modules.Vector3
{

	[Serializable]
	public class UnityEvent : Vec3Module,
	                          IEventModule<UnityEngine.Vector3>
	{

		[SerializeField]
		public UnityEvent<UnityEngine.Vector3> onValueChange = new();
		public UnityEvent<UnityEngine.Vector3> OnValueChange => onValueChange;

		public override void OnValidate(UnityEngine.Vector3 value) => OnValueChange.Invoke(value);

		public override void Init(DriverSet driverSet) { }

		public override void HandleValueChange(UnityEngine.Vector3 value) => OnValueChange.Invoke(value);

	}

}
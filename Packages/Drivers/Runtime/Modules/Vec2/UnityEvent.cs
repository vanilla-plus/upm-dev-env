using System;

using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Modules.Vector2
{

	[Serializable]
	public class UnityEvent : Vec2Module,
	                          IEventModule<UnityEngine.Vector2>
	{

		[SerializeField]
		public UnityEvent<UnityEngine.Vector2> onValueChange = new();
		public UnityEvent<UnityEngine.Vector2> OnValueChange => onValueChange;

		public override void OnValidate(UnityEngine.Vector2 value) => OnValueChange.Invoke(value);

		public override void Init(DriverSet driverSet) { }

		public override void HandleValueChange(UnityEngine.Vector2 value) => OnValueChange.Invoke(value);

	}

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Bool
{
	[Serializable]
	public class Unity_Event : Module
	{

		[SerializeField]
		public UnityEvent<bool> onValueChange = new();
		public UnityEvent<bool> OnValueChange => onValueChange;

		public override void OnValidate(Driver<bool> driver) => OnValueChange.Invoke(driver.Asset.Delta.Value);

		public override void Init(Driver<bool> driver) => HandleValueChange(driver.Asset.Delta.Value);

		public override void HandleValueChange(bool value) => OnValueChange.Invoke(value);

	}
}
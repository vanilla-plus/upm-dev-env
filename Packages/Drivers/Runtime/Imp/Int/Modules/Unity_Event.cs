using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Int
{
	[Serializable]
	public class Unity_Event : Module
	{

		[SerializeField]
		public UnityEvent<int> onValueChange = new();
		public UnityEvent<int> OnValueChange => onValueChange;

		public override void OnValidate(Driver<int> driver) => OnValueChange.Invoke(driver.Asset.Delta.Value);

		public override void Init(Driver<int> driver) => HandleValueChange(driver.Asset.Delta.Value);

		public override void HandleValueChange(int value) => OnValueChange.Invoke(value);

	}
}
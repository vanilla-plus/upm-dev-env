using System;

using UnityEngine.Events;

namespace Vanilla.MetaScript.Drivers.Bool
{

	[Serializable]
	public class Unity_Event : Module
	{

		public UnityEvent<bool> OnSet = new();

		public override void OnValidate(Driver driver) => OnSet.Invoke(driver.Asset.Source.Value);

		public override void Init(Driver driver) => TryConnectSet(driver);

		public override void DeInit(Driver driver) => TryDisconnectSet(driver);

		protected override void HandleSet(bool value) => OnSet.Invoke(value);

	}

}
using System;

using UnityEngine.Events;

namespace Vanilla.MetaScript.Drivers.Int
{
	[Serializable]
	public class Unity_Event : Module
	{

		public UnityEvent<int> OnSet = new();

		public override void OnValidate(Driver<int> driver) => OnSet.Invoke(driver.Asset.Source.Value);

		public override void Init(Driver<int> driver) => TryConnectSet(driver);

		public override void DeInit(Driver<int> driver) => TryDisconnectSet(driver);

		protected override void HandleSet(int value) => OnSet.Invoke(value);

	}
}
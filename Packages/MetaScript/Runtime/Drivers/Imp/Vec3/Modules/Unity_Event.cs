using System;

using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.MetaScript.Drivers.Vec3
{

	[Serializable]
	public class Unity_Event : Module
	{

		public UnityEvent<Vector3> OnSet = new();

        public override void OnValidate(Driver<Vector3> driver) => OnSet.Invoke(driver.Asset.Source.Value);

		public override void Init(Driver<Vector3> driver) => TryConnectSet(driver);

		public override void DeInit(Driver<Vector3> driver) => TryDisconnectSet(driver);

		protected override void HandleSet(Vector3 value) => OnSet.Invoke(value);

	}

}
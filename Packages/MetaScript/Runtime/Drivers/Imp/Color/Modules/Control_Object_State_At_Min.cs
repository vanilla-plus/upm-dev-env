using System;

using UnityEngine;

namespace Vanilla.MetaScript.Drivers.Color
{

	[Serializable]
	public class Control_Object_State_With_Alpha : Module
	{

		[SerializeField]
		public GameObject[] controlledObjects = Array.Empty<GameObject>();

		public override void OnValidate(Driver<UnityEngine.Color> driver)
		{
			#if UNITY_EDITOR
			if (!ValidReferences(driver)) return;

			var shouldBeActive = driver.Asset.Source.Value.a > Mathf.Epsilon;

			foreach (var g in controlledObjects)
			{
				if (g != null) g.SetActive(shouldBeActive);
			}
			#endif
		}


		public override void Init(Driver<UnityEngine.Color> driver) => TryConnectSet(driver);

		public override void DeInit(Driver<UnityEngine.Color> driver) => TryDisconnectSet(driver);
		

		protected override void HandleSet(UnityEngine.Color incoming)
		{
			var shouldBeActive = incoming.a > Mathf.Epsilon;
			
			foreach (var g in controlledObjects)
				if (g != null)
					g.SetActive(shouldBeActive);
		}

	}

}
using System;

using UnityEngine;

namespace Vanilla.Drivers.Float
{
	[Serializable]
	public class Log_Value : Module
	{

		[SerializeField]
		public bool IncludeHistory = false;
		
		[SerializeField]
		[HideInInspector]
		private string DriverName;
		
		public override void OnValidate(Driver<float> driver)
		{
			if (!ValidReferences(driver)) return;

			DriverName = driver.Asset.name;
		}


		public override void Init(Driver<float> driver)
		{
			if (!IncludeHistory)
			{
				TryConnectSet(driver);
			}
			else
			{
				TryConnectSetWithHistory(driver);
			}
		}


		public override void DeInit(Driver<float> driver)
		{
			if (!IncludeHistory)
			{
				TryDisconnectSet(driver);
			}
			else
			{
				TryDisconnectSetWithHistory(driver);
			}
		}

		protected override void HandleSet(float incoming) => Debug.Log($"[{DriverName}] was set to [{incoming}]");

		protected override void HandleSetWithHistory(float incoming,
		                                             float outgoing) => Debug.Log($"[{DriverName}] was set from [{outgoing}] to [{incoming}]");



	}
}
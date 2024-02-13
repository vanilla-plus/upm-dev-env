using System;

using UnityEngine;

namespace Vanilla.Drivers.Bool
{
	[Serializable]
	public class Log_Value : Module
	{

		[SerializeField]
		public bool IncludeHistory = false;
		
		[SerializeField]
		[HideInInspector]
		private string DriverName;
		
		public override void OnValidate(Driver<bool> driver)
		{
			if (!ValidReferences(driver)) return;

			DriverName = driver.Asset.name;
		}


		public override void Init(Driver<bool> driver)
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


		public override void DeInit(Driver<bool> driver)
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

		protected override void HandleSet(bool incoming) => Debug.Log($"[{DriverName}] was set to [{incoming}]");

		protected override void HandleSetWithHistory(bool incoming,
		                                             bool outgoing) => Debug.Log($"[{DriverName}] was set from [{outgoing}] to [{incoming}]");



	}
}
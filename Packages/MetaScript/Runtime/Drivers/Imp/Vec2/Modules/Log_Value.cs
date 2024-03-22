using System;

using UnityEngine;

namespace Vanilla.MetaScript.Drivers.Vec2
{
	[Serializable]
	public class Log_Value : Module
	{

		[SerializeField]
		public bool IncludeHistory = false;
		
		[SerializeField]
		[HideInInspector]
		private string DriverName;
		
		public override void OnValidate(Driver driver)
		{
			if (!ValidReferences(driver)) return;

			DriverName = driver.Asset.name;
		}


		public override void Init(Driver driver)
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


		public override void DeInit(Driver driver)
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

		protected override void HandleSet(Vector2 incoming) => Debug.Log($"[{DriverName}] was set to [{incoming}]");

		protected override void HandleSetWithHistory(Vector2 incoming,
		                                             Vector2 outgoing) => Debug.Log($"[{DriverName}] was set from [{outgoing}] to [{incoming}]");



	}
}
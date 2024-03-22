using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers.Float
{

	[Serializable]
	public class Control_Object_State_At_Min : Module
	{

		[SerializeField]
		public GameObject[] controlledObjects = Array.Empty<GameObject>();

		public override void OnValidate(Driver driver)
		{
			#if UNITY_EDITOR
			if (!ValidReferences(driver)) return;
			
			if (driver.Asset.Source is not IRangedDataSource<float> source)
			{
				Debug.LogError("OnValidate failed because the drivers Asset.Source is not of type RangedDataSource<T>");
				
				return;
			}
            
			var shouldBeActive = !source.AtMin.Value;

			foreach (var g in controlledObjects)
			{
				if (g != null) g.SetActive(shouldBeActive);
			}
			#endif
		}


		public override void Init(Driver driver)
		{
			if (!ValidReferences(driver)) return;

			TryConnectAtMinSet(driver);
		}
		
		public override void DeInit(Driver driver) => TryDisconnectAtMinSet(driver);


		protected override void HandleAtMinSet(bool atMin)
		{
			var shouldBeActive = !atMin;

			foreach (var g in controlledObjects)
				if (g != null)
					g.SetActive(shouldBeActive);
		}

	}

}
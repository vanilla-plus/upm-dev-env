using System;

using UnityEngine;
using UnityEngine.VFX;

namespace Vanilla.Drivers.Bool
{
	[Serializable]
	public class Set_VFX_Graph : Module
	{

		[SerializeField]
		private string propertyName = string.Empty;
		public string PropertyName
		{
			get => propertyName;
			set => propertyName = value;
		}

		[SerializeField]
		private int propertyID = -1;
		public int PropertyID
		{
			get => propertyID;
			set => propertyID = value;
		}

		[SerializeField]
		private VisualEffect[] graphs;
		public VisualEffect[] Graphs
		{
			get => graphs;
			set => graphs = value;
		}


		public override void OnValidate(Driver<bool> driver)
		{
			#if UNITY_EDITOR
			PropertyID = Shader.PropertyToID(PropertyName);

			// Is it safe to set VFXGraph values outside of Play Mode? Let's find out.

			HandleValueChange(driver.Asset.Delta.Value);
			#endif
		}
		
		public override void Init(Driver<bool> driver)
		{
			PropertyID = Shader.PropertyToID(PropertyName);
            
			base.Init(driver: driver);
		}


		public override void HandleValueChange(bool value)
		{
			foreach (var g in Graphs)
			{
				if (g != null)
					g.SetBool(nameID: PropertyID,
					          b: value);
			}
		}
		
	}
}
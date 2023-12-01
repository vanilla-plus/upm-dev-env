using System;

using UnityEngine;
using UnityEngine.VFX;

namespace Vanilla.Drivers.Modules.Color
{

	[Serializable]
	public class Set_VFX_Graph : ColorModule, 
	                             IVFXGraphModule
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


		public override void OnValidate(UnityEngine.Color value)
		{
			#if UNITY_EDITOR
			PropertyID = Shader.PropertyToID(PropertyName);

			// Is it safe to set VFXGraph values outside of Play Mode? Let's find out.

			HandleValueChange(value);
			#endif
		}
		
		public override void Init(DriverSet driverSet) => PropertyID = Shader.PropertyToID(PropertyName);
		
		public override void HandleValueChange(UnityEngine.Color value)
		{
			foreach (var g in Graphs)
			{
				if (g != null)
					g.SetVector4(nameID: PropertyID,
					             v: value);
			}
		}
		
	}

}
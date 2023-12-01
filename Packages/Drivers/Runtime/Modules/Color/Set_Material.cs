using System;

using UnityEngine;

namespace Vanilla.Drivers.Modules.Color
{

	// Warning - This is hacktown, USA.
	// Unity is like "cant call SetColorImp during serialization
	// so we don't, we call SetColor, without telling mummy.
	// And it works fine? So like shut up Unity?? Just kill the imp

	[Serializable]
	public class Set_Material : ColorModule,
	                            IMaterialModule
	{

		[SerializeField]
		private string propertyName = "_BaseColor";
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
		private Material[] materials = Array.Empty<Material>();
		public Material[] Materials => materials;


		public override void OnValidate(UnityEngine.Color value)
		{
			PropertyID = Shader.PropertyToID(PropertyName);

			#if UNITY_EDITOR
			foreach (var material in materials)
			{
				if (material == null) continue;

				var setMethod = typeof(Material).GetMethod(name: "SetColor",
				                                           types: new[]
				                                                  {
					                                                  typeof(string),
					                                                  typeof(UnityEngine.Color)
				                                                  });

				setMethod?.Invoke(obj: material,
				                  parameters: new object[]
				                              {
					                              PropertyName,
					                              value
				                              });
			}
			#endif
		}


		public override void Init(DriverSet driverSet) => PropertyID = Shader.PropertyToID(PropertyName);


		public override void HandleValueChange(UnityEngine.Color value)
		{
			foreach (var material in materials)
			{
				if (material != null)
				{
					material.SetColor(nameID: PropertyID,
					                  value: value);
				}
			}
		}

	}

}
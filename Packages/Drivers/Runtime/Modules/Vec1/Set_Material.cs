using System;

using UnityEngine;

namespace Vanilla.Drivers.Snrubs.Vector1
{

	[Serializable]
	public class Set_Material : Vec1Snrub,
	                            IMaterialSnrub
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
		private Material[] materials = Array.Empty<Material>();
		public Material[] Materials => materials;

		// Warning - This is hacktown, USA.
		// Unity is like "cant call SetColorImp during serialization
		// so we don't, we call SetColor, without telling mummy.
		// And it works fine? So like shut up Unity?? Just kill the imp

		// Okay, this is a bit of a mouthful, but this will automatically
		// also turn off any target GameObjects if the "get" value is 0.0f

		public override void OnValidate(float value)
		{
			PropertyID = Shader.PropertyToID(PropertyName);

			#if UNITY_EDITOR
			foreach (var material in materials)
			{
				if (material == null) continue;

				var setMethod = typeof(Material).GetMethod(name: "SetFloat",
				                                           types: new[]
				                                                  {
					                                                  typeof(string),
					                                                  typeof(float)
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
		
		public override void Init(DriverSocket<float> socket) => PropertyID = Shader.PropertyToID(PropertyName);

		public override void HandleValueChange(float value)
		{
			foreach (var material in materials)
			{
				if (material != null)
				{
					material.SetFloat(nameID: PropertyID,
					                  value: value);
				}
			}
		}

	}

}
using System;

using UnityEngine;

namespace Vanilla.Drivers.Color
{

	// Warning - This is hacktown, USA.
	// Unity is like "cant call SetUnityEngine.ColorImp during serialization
	// so we don't, we call SetUnityEngine.Color, without telling mummy.
	// And it works fine? So like shut up Unity?? Just kill the imp
	
	[Serializable]
	public class Set_Material_W_State_Control : Module
	{

		[SerializeField]
		private string propertyName = "_BaseUnityEngine.Color";
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

		[SerializeField]
		public GameObject[] controlledObjects = Array.Empty<GameObject>();

		public override void OnValidate(Driver<UnityEngine.Color> driver)
		{
			PropertyID = Shader.PropertyToID(PropertyName);

			var value = driver.Asset.Delta.Value;

			#if UNITY_EDITOR
			foreach (var material in materials)
			{
				if (material == null) continue;

				var setMethod = typeof(Material).GetMethod(name: "SetUnityEngine.Color",
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
			
			var fullyTransparent = value.a < Mathf.Epsilon;
			
			foreach (var g in controlledObjects)
				if (g != null)
					g.SetActive(!fullyTransparent);
			#endif
		}

		public override void Init(Driver<UnityEngine.Color> driver) => PropertyID = Shader.PropertyToID(PropertyName);
		
		public override void HandleValueChange(UnityEngine.Color value)
		{
			var fullyTransparent = value.a < Mathf.Epsilon;
			
			foreach (var g in controlledObjects)
				if (g != null)
					g.SetActive(!fullyTransparent);
			
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
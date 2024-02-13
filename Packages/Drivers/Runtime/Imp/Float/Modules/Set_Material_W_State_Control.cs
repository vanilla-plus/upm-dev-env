using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.Drivers.Float
{
    
    [Serializable]
    public class Set_Material_W_State_Control : Module
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

		[SerializeField]
		public GameObject[] controlledObjects = Array.Empty<GameObject>();
		
		// Warning - This is hacktown, USA.
		// Unity is like "cant call SetColorImp during serialization
		// so we don't, we call SetColor, without telling mummy.
		// And it works fine? So like shut up Unity?? Just kill the imp

		// Okay, this is a bit of a mouthful, but this will automatically
		// also turn off any target GameObjects if the "get" value is 0.0f
		
		public override void OnValidate(Driver<float> driver)
		{
			if (driver.Asset == null) return;

			var value = driver.Asset.Source.Value;
			
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

			var fullyTransparent = value < Mathf.Epsilon;
			
			foreach (var g in controlledObjects)
				if (g != null)
					g.SetActive(!fullyTransparent);
			#endif
		}


		public override void Init(Driver<float> driver)
		{
			PropertyID = Shader.PropertyToID(PropertyName);

			base.Init(driver);
		}


		public override void HandleValueChange(float value)
		{
			var fullyTransparent = value < Mathf.Epsilon;
			
			foreach (var g in controlledObjects)
				if (g != null)
					g.SetActive(!fullyTransparent);
			
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

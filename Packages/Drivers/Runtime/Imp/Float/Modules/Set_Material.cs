using System;

using UnityEngine;

namespace Vanilla.Drivers.Float
{
    
    [Serializable]
    public class Set_Material : Module
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

		// Warning - This is hacktown, USA.
		// Unity is like "cant call SetColorImp during serialization
		// so we don't, we call SetColor, without telling mummy.
		// And it works fine? So like shut up Unity?? Just kill the imp

		public override void OnValidate(Driver<float> driver)
		{
			if (driver.Asset == null) return;

			var value = driver.Asset.Source.Value;
			
			// Even though these need setting in builds (and are NOT platform-agnostic)
			// We can still set them temporarily here so that changes can be
			// reflected in the Editor (which has it's own PropertyID)
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


		public override void Init(Driver<float> driver)
		{
			PropertyID = Shader.PropertyToID(PropertyName);

			TryConnectSet(driver);
		}

		public override void DeInit(Driver<float> driver) => TryDisconnectSet(driver);

		protected override void HandleSet(float value)
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

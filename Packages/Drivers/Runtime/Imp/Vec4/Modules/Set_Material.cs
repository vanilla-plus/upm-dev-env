using System;

using UnityEngine;

namespace Vanilla.Drivers.Vec4
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

		// Unity is like "cant call SetColorImp during serialization
		// so we don't, we call SetColor, without telling mummy.
		// And it works fine? So like shut up Unity?? Just kill the imp

		public override void OnValidate(Driver<Vector4> driver)
		{
			if (!ValidReferences(driver)) return;
			
			if (driver.Asset == null) return;

			var value = driver.Asset.Source.Value;
			
			#if UNITY_EDITOR
			foreach (var material in materials)
			{
				if (material == null) continue;

				var setMethod = typeof(Material).GetMethod(name: "SetVector",
				                                           types: new[]
				                                                  {
					                                                  typeof(string),
					                                                  typeof(Vector4)
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

		

		public override void Init(Driver<Vector4> driver)
		{
			PropertyID = Shader.PropertyToID(PropertyName);

			TryConnectSet(driver);
		}


		public override void DeInit(Driver<Vector4> driver) => TryDisconnectSet(driver);


		protected override void HandleSet(Vector4 value)
		{
			foreach (var material in materials)
			{
				if (material != null)
				{
					material.SetVector(nameID: PropertyID,
					                  value: value);
				}
			}
		}

    }
}

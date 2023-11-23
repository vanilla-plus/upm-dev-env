using System;

using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Modules.Vector1
{


	[Serializable]
	public class UnityEvent : Vec1Module,
	                          IEventModule<float>
	{

		[SerializeField]
		public UnityEvent<float> onValueChange = new();
		public UnityEvent<float> OnValueChange => onValueChange;

		public override void OnValidate(float value) => OnValueChange.Invoke(value);

		public override void HandleValueChange(float value) => OnValueChange.Invoke(value);

	}
	
	// Warning - This is hacktown, USA.
	// Unity is like "cant call SetColorImp during serialization
	// so we don't, we call SetColor, without telling mummy.
	// And it works fine? So like shut up Unity?? Just kill the imp

	[Serializable]
	public class Set_Material : Vec1Module,
	                            IMaterialModule
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
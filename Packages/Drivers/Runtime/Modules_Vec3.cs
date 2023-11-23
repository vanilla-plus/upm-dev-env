using System;

using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Modules.Vector3
{


	[Serializable]
	public class UnityEvent : Vec3Module,
	                          IEventModule<UnityEngine.Vector3>
	{

		[SerializeField]
		public UnityEvent<UnityEngine.Vector3> onValueChange = new();
		public UnityEvent<UnityEngine.Vector3> OnValueChange => onValueChange;

		public override void OnValidate(UnityEngine.Vector3 value) => OnValueChange.Invoke(value);

		public override void HandleValueChange(UnityEngine.Vector3 value) => OnValueChange.Invoke(value);

	}

	// Warning - This is hacktown, USA.
	// Unity is like "cant call SetColorImp during serialization
	// so we don't, we call SetColor, without telling mummy.
	// And it works fine? So like shut up Unity?? Just kill the imp

	[Serializable]
	public class Set_Material : Vec3Module,
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


		public override void OnValidate(UnityEngine.Vector3 value)
		{
			PropertyID = Shader.PropertyToID(PropertyName);

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


		public override void HandleValueChange(UnityEngine.Vector3 value) { }

	}

}
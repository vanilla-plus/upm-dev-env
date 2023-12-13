//using System;
//
//using UnityEngine;
//
//namespace Vanilla.Drivers.Snrubs.Color
//{
//
//	// Warning - This is hacktown, USA.
//	// Unity is like "cant call SetColorImp during serialization
//	// so we don't, we call SetColor, without telling mummy.
//	// And it works fine? So like shut up Unity?? Just kill the imp
//	
//	[Serializable]
//	public class Set_Material_Turn_Off_If_Transparent : ColorSnrub,
//	                                                    IMaterialSnrub
//	{
//
//		[SerializeField]
//		private string propertyName = "_BaseColor";
//		public string PropertyName
//		{
//			get => propertyName;
//			set => propertyName = value;
//		}
//
//		[SerializeField]
//		private int propertyID = -1;
//		public int PropertyID
//		{
//			get => propertyID;
//			set => propertyID = value;
//		}
//
//		[SerializeField]
//		private Material[] materials = Array.Empty<Material>();
//		public Material[] Materials => materials;
//
//		[SerializeField]
//		public GameObject[] controlledObjects = Array.Empty<GameObject>();
//
//		public override void OnValidate(UnityEngine.Color value)
//		{
//			PropertyID = Shader.PropertyToID(PropertyName);
//
//			#if UNITY_EDITOR
//			foreach (var material in materials)
//			{
//				if (material == null) continue;
//
//				var setMethod = typeof(Material).GetMethod(name: "SetColor",
//				                                           types: new[]
//				                                                  {
//					                                                  typeof(string),
//					                                                  typeof(UnityEngine.Color)
//				                                                  });
//
//				setMethod?.Invoke(obj: material,
//				                  parameters: new object[]
//				                              {
//					                              PropertyName,
//					                              value
//				                              });
//			}
//			
//			var fullyTransparent = value.a < Mathf.Epsilon;
//			
//			foreach (var g in controlledObjects)
//				if (g != null)
//					g.SetActive(!fullyTransparent);
//			#endif
//		}
//
//		public override void Init(Vec1DriverSocket vec1DriverSocket) => PropertyID = Shader.PropertyToID(PropertyName);
//		
//		public override void HandleValueChange(UnityEngine.Color value)
//		{
//			var fullyTransparent = value.a < Mathf.Epsilon;
//			
//			foreach (var g in controlledObjects)
//				if (g != null)
//					g.SetActive(!fullyTransparent);
//			
//			foreach (var material in materials)
//			{
//				if (material != null)
//				{
//					material.SetColor(nameID: PropertyID,
//					                  value: value);
//				}
//			}
//		}
//
//	}
//
//}
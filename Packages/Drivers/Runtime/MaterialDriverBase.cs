//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//using Vanilla.MetaScript;
//
//namespace Vanilla.Drivers
//{
//
////    
////    // tl;dr make a generic base class that just "gets driven"
////    // and then overload it with a bunch of variations for Unity Event (EventDriver?) and MaterialDrivers
////    // tbh generic shit shouldn't even come into it, who cares what each one does, you have typemenu
////    
////    public interface IMaterialDriver
////    {
////
////        string Name
////        {
////            get;
////            set;
////        }
////
////        Material[] TargetMaterials
////        {
////            get;
////            set;
////        }
////
////    }
////    
////    public class MaterialDriverBase : IDriver
////    {
////
////        [SerializeField]
////        public string Name;
////
////        public void Init(Normal normal)
////        {
////            normal.OnValueChanged += Interpolate;
////
////            Interpolate(normal.Value);
////        }
////
////
////        public void DeInit(Normal normal) => normal.OnValueChanged -= Interpolate;
////
////        public void Interpolate(float normal) => OnValueChange.Invoke(Get(normal));
////
////        protected abstract T Get(float normal);
////
////    }
//
//	[Serializable]
//	public abstract class MaterialDriver<T> : DriverBase<T>
//	{
//
//		[SerializeField]
//		public string PropertyName = "_BaseColor";
//
//		[HideInInspector]
//		[SerializeField]
//		protected int PropertyID;
//
//		[SerializeField]
//		public Material[] materials = Array.Empty<Material>();
//
//
//		public override void Init(DriverSet set)
//		{
//			PropertyID = Shader.PropertyToID(PropertyName);
//
//			base.Init(set);
//		}
//
//
//		public override void Interpolate(float normal)
//		{
//			var value = Get(normal);
//
//			foreach (var material in materials)
//				Set(material: material,
//				    value: value);
//		}
//
//
//		protected abstract void Set(Material material,
//		                            T value);
//
//	}
//
//	[Serializable]
//	public class MaterialVec1Driver : MaterialDriver<float>
//	{
//
//		
//		[SerializeField]
//		public float Min = 0.0f;
//
//		[SerializeField]
//		public float Max = 1.0f;
//
//		public override void OnValidate(DriverSet driverSet) { }
//
//		protected override float Get(float normal) => Mathf.Lerp(a: Min,
//		                                                         b: Max,
//		                                                         t: normal);
//
//
//		protected override void Set(Material material,
//		                            float value) => material.SetFloat(nameID: PropertyID,
//		                                                              value: value);
//
//	}
//
//	[Serializable]
//	public class MaterialVec2Driver : MaterialDriver<Vector2>
//	{
//
//
//		[SerializeField]
//		public Vector2 Min = Vector2.zero;
//
//		[SerializeField]
//		public Vector2 Max = Vector2.one;
//
//
//		public override void OnValidate(DriverSet driverSet) { }
//
//		protected override Vector2 Get(float normal) => Vector2.Lerp(a: Min,
//		                                                             b: Max,
//		                                                             t: normal);
//
//
//		protected override void Set(Material material,
//		                            Vector2 value) => material.SetVector(nameID: PropertyID,
//		                                                                 value: value);
//
//	}
//	
//	[Serializable]
//	public class MaterialVec3Driver : MaterialDriver<Vector3>
//	{
//
//		[SerializeField]
//		public Vector3 Min = Vector3.zero;
//
//		[SerializeField]
//		public Vector3 Max = Vector3.one;
//
//		public override void OnValidate(DriverSet driverSet) { }
//
//		protected override Vector3 Get(float normal) => Vector3.Lerp(a: Min,
//		                                                             b: Max,
//		                                                             t: normal);
//
//
//		protected override void Set(Material material,
//		                            Vector3 value) => material.SetVector(nameID: PropertyID,
//		                                                                 value: value);
//
//	}
//
//	[Serializable]
//	public class MaterialColorDriver : MaterialDriver<Color>
//	{
//		
//		// ToDo - Chuck this back in! You're almost done.
//		public override void OnValidate(DriverSet set)
//		{
//			#if UNITY_EDITOR
//			if (!Application.isPlaying)
//			{
//				var value = Get(set.normal);
//
//				// Warning - This is hacktown, USA.
//				// Unity is like "cant call SetColorImp during serialization
//				// so we don't, we call SetColor, without telling mummy.
//				// And it works fine? So like shut up Unity?? Just kill the imp
//				foreach (var material in materials)
//				{
//					if (material == null) continue;
//
//					// Get the SetColor method
//					var setColorMethod = typeof(Material).GetMethod(name: "SetColor",
//					                                                types: new[]
//					                                                       {
//						                                                       typeof(string),
//						                                                       typeof(Color)
//					                                                       });
//
//					// Invoke the method with the property name and the new color
//					setColorMethod?.Invoke(obj: material,
//					                       parameters: new object[]
//					                                   {
//						                                   PropertyName,
//						                                   value
//					                                   });
//				}
//			}
//			#endif
//		}
//
//		[SerializeField]
//		public Color From = Color.black;
//
//		[SerializeField]
//		public Color To = Color.white;
//
//
//		protected override Color Get(float normal) => Color.Lerp(a: From,
//		                                                         b: To,
//		                                                         t: normal);
//
//
//		protected override void Set(Material material,
//		                            Color value) => material.SetColor(nameID: PropertyID,
//		                                                              value: value);
//
//	}
//	
//	[Serializable]
//	public class MaterialGradientDriver : MaterialDriver<Color>
//	{
//
//		[SerializeField]
//		public Gradient[] gradients = Array.Empty<Gradient>();
//		
//		public override void OnValidate(DriverSet set) { }
//		
//		protected override Color Get(float normal)
//		{
//			var gradientCount = gradients.Length;
//
//			if (gradientCount == 0) return Color.white;
//
//			var gradientBracket = (int) Mathf.Clamp(value: Mathf.Floor(f: normal * gradientCount),
//			                                        min: 0,
//			                                        max: gradientCount - 1);
//
//			var output = normal * gradientCount - gradientBracket;
//
//			return gradients[gradientBracket].Evaluate(time: output);
//		}
//
//
//		protected override void Set(Material material,
//		                            Color value) => material.SetColor(nameID: PropertyID,
//		                                                              value: value);
//
//	}
//
//}
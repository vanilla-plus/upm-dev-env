using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers
{
    
    [Serializable]
    public abstract class ModuleBase<T>
    {

	    public abstract void OnValidate(T value);

        public abstract void HandleValueChange(T value);

    }

    [Serializable] public abstract class Vec1Module : ModuleBase<float> { }
    [Serializable] public abstract class Vec2Module : ModuleBase<Vector2> { }
    [Serializable] public abstract class Vec3Module : ModuleBase<Vector3> { }

    [Serializable] public abstract class ColorModule : ModuleBase<Color> { }

    public interface IEventModule<T>
    {

        UnityEvent<T> OnValueChange
        {
	        get;
        }

        void OnValidate(T value) => OnValueChange.Invoke(value);
        
    }

    [Serializable]
    public class UnityEvent_Vec1 : Vec1Module,
                                   IEventModule<float>
    {

	    [SerializeField]
	    public UnityEvent<float> onValueChange = new();
	    public UnityEvent<float> OnValueChange => onValueChange;

	    public override void OnValidate(float value) => OnValueChange.Invoke(value);

	    public override         void HandleValueChange(float value) => OnValueChange.Invoke(value);
	    
    }
    
    [Serializable]
    public class UnityEvent_Vec2 : Vec2Module,
                                   IEventModule<Vector2>
    {

	    [SerializeField]
	    public UnityEvent<Vector2> onValueChange = new();
	    public UnityEvent<Vector2> OnValueChange => onValueChange;

	    public override void OnValidate(Vector2 value) => OnValueChange.Invoke(value);

	    public override         void HandleValueChange(Vector2 value) => OnValueChange.Invoke(value);
	    
    }
        
    [Serializable]
    public class UnityEvent_Vec3 : Vec3Module,
                                   IEventModule<Vector3>
    {

	    [SerializeField]
	    public UnityEvent<Vector3> onValueChange = new();
	    public UnityEvent<Vector3> OnValueChange => onValueChange;

	    public override void OnValidate(Vector3 value) => OnValueChange.Invoke(value);

	    public override         void HandleValueChange(Vector3 value) => OnValueChange.Invoke(value);
	    
    }

    [Serializable]
    public class UnityEvent_Color : ColorModule,
                                    IEventModule<Color>
    {

	    [SerializeField]
	    public UnityEvent<Color> onValueChange = new();
	    public UnityEvent<Color> OnValueChange => onValueChange;

	    public override void OnValidate(Color value) => OnValueChange.Invoke(value);

	    public override         void HandleValueChange(Color value) => OnValueChange.Invoke(value);

    }

//    [Serializable]
//    public abstract class EventModule<T> : ModuleBase<T>
//    {
//
//        [SerializeField]
//        public UnityEvent<T> OnValueChange;
//
//        public override void Interpolate(T value) => OnValueChange.Invoke(value);
//
//    }

//    [Serializable] public class UnityEvent_Vec1 : EventModule<float> { }
//    [Serializable] public class UnityEvent_Vec2 : EventModule<Vector2> { }
//    [Serializable] public class UnityEvent_Vec3 : EventModule<Vector3> { }
//    [Serializable] public class UnityEvent_Color : EventModule<Color> { }
//
//    [Serializable]
//    public abstract class MaterialModule<T> : ModuleBase<T>
//    {
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
//		public override void HandleValueChange(T value) => SetMaterialValue(value);
//
//		public abstract void SetMaterialValue(T value);
//
//    }
//
//    [Serializable]
//    public class Set_Material_Vec1 : MaterialModule<float>
//    {
//
//	    public override void SetMaterialValue(float value)
//	    {
//		    foreach (var m in materials)
//		    {
//			    if (m != null)
//			    {
//				    m.SetFloat(nameID: PropertyID,
//				               value: value);
//			    }
//		    }
//	    }
//
//    }

	public interface IMaterialModule
	{
		
		string PropertyName
		{
			get;
			set;
		}

		int PropertyID
		{
			get;
			set;
		}
		
		Material[] Materials
		{
			get;
		}

	}

	[Serializable]
	public class Set_Material_Vec1 : Vec1Module,
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
//			if (!Application.isPlaying)
//			{
				// Warning - This is hacktown, USA.
				// Unity is like "cant call SetColorImp during serialization
				// so we don't, we call SetColor, without telling mummy.
				// And it works fine? So like shut up Unity?? Just kill the imp
				foreach (var material in materials)
				{
					if (material == null) continue;

					// Get the SetColor method
					var setMethod = typeof(Material).GetMethod(name: "SetFloat",
					                                                types: new[]
					                                                       {
						                                                       typeof(string),
						                                                       typeof(float)
					                                                       });

					// Invoke the method with the property name and the new color
					setMethod?.Invoke(obj: material,
					                       parameters: new object[]
					                                   {
						                                   PropertyName,
						                                   value
					                                   });
				}
//			}
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
    
	[Serializable]
	public class Set_Material_Vec2 : Vec2Module,
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

		public override void OnValidate(Vector2 value)
		{
			PropertyID = Shader.PropertyToID(PropertyName);

			#if UNITY_EDITOR
//			if (!Application.isPlaying)
//			{
				// Warning - This is hacktown, USA.
				// Unity is like "cant call SetColorImp during serialization
				// so we don't, we call SetColor, without telling mummy.
				// And it works fine? So like shut up Unity?? Just kill the imp
				foreach (var material in materials)
				{
					if (material == null) continue;

					// Get the SetColor method
					var setMethod = typeof(Material).GetMethod(name: "SetVector",
					                                                types: new[]
					                                                       {
						                                                       typeof(string),
						                                                       typeof(Vector4) // Vector4 right?
					                                                       });

					// Invoke the method with the property name and the new color
					setMethod?.Invoke(obj: material,
					                       parameters: new object[]
					                                   {
						                                   PropertyName,
						                                   value
					                                   });
				}
//			}
			#endif
		}

		public override         void HandleValueChange(Vector2 value) { }

	}
        
	[Serializable]
	public class Set_Material_Vec3 : Vec3Module,
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

		public override void OnValidate(Vector3 value)
		{
			PropertyID = Shader.PropertyToID(PropertyName);

			#if UNITY_EDITOR
//			if (!Application.isPlaying)
//			{
				// Warning - This is hacktown, USA.
				// Unity is like "cant call SetColorImp during serialization
				// so we don't, we call SetColor, without telling mummy.
				// And it works fine? So like shut up Unity?? Just kill the imp
				foreach (var material in materials)
				{
					if (material == null) continue;

					// Get the SetColor method
					var setMethod = typeof(Material).GetMethod(name: "SetVector",
					                                                types: new[]
					                                                       {
						                                                       typeof(string),
						                                                       typeof(Vector4) // Vector4 right?
					                                                       });

					// Invoke the method with the property name and the new color
					setMethod?.Invoke(obj: material,
					                       parameters: new object[]
					                                   {
						                                   PropertyName,
						                                   value
					                                   });
				}
//			}
			#endif
		}

		public override         void HandleValueChange(Vector3 value) { }

	}

	[Serializable]
	public class Set_Material_Color : ColorModule,
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

		public override void OnValidate(Color value)
		{
			PropertyID = Shader.PropertyToID(PropertyName);
			
			#if UNITY_EDITOR
//			if (!Application.isPlaying)
//			{
				// Warning - This is hacktown, USA.
				// Unity is like "cant call SetColorImp during serialization
				// so we don't, we call SetColor, without telling mummy.
				// And it works fine? So like shut up Unity?? Just kill the imp
				foreach (var material in materials)
				{
					if (material == null) continue;

					// Get the SetColor method
					var setMethod = typeof(Material).GetMethod(name: "SetColor",
					                                                types: new[]
					                                                       {
						                                                       typeof(string),
						                                                       typeof(Color) // Vector4 right?
					                                                       });

					// Invoke the method with the property name and the new color
					setMethod?.Invoke(obj: material,
					                       parameters: new object[]
					                                   {
						                                   PropertyName,
						                                   value
					                                   });
				}
//			}
			#endif
		}

		public override         void HandleValueChange(Color value) { }

	}

}
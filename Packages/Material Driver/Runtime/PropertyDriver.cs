using System;

using UnityEngine;

namespace Vanilla.MaterialDriver
{

	[Serializable]
	public abstract class PropertyDriver : ISerializationCallbackReceiver
	{

		[SerializeField]
		public string Name;

		[SerializeField]
		public bool InvertNormal = false;
		
		[SerializeField]
		protected int PropertyId;

		[SerializeField]
		public Material[] TargetMaterials = Array.Empty<Material>();

		public void OnBeforeSerialize() { }


		public virtual void OnAfterDeserialize() => PropertyId = Shader.PropertyToID(Name);

		public abstract void Init(float normal); // Connect OnValueChange event
		public abstract void DeInit(); // Disconnect OnValueChange event

		public abstract void Lerp(float normal);
		
		public abstract void Lerp(float outgoing, float incoming);

		public abstract void OnValidate(float normal);

	}

}
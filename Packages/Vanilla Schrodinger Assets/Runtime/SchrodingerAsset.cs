using System;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.SchrodingerAssets
{

	[CreateAssetMenu(fileName = "New Schrodinger Asset",
	                 menuName = "Vanilla/Schrodinger Asset")]
	public class SchrodingerAsset : ScriptableObject
	{

		[SerializeReference] 
		[TypeMenu]
		public IPayload[] payloads;

	}

	public interface IPayload
	{

		T Get<T>();

		void Set<T>(T input);

	}

	[Serializable]
	public class BoolPayload : IPayload
	{

		[SerializeField]
		public bool value;

		public T Get<T>() => value is T f ? f : default;

		public void Set<T>(T input) => value = input is bool v ? v : default;

	}

	[Serializable]
	public class FloatPayload : IPayload
	{

		[SerializeField]
		public float value;

		public T Get<T>() => value is T f ? f : default;

		public void Set<T>(T input) => value = input is float v ? v : default;

	}
	
	[Serializable]
	public class IntPayload : IPayload
	{

		[SerializeField]
		public int value;

		public T Get<T>() => value is T f ? f : default;

		public void Set<T>(T input) => value = input is int v ? v : default;

	}
	
	[Serializable]
	public class StringPayload : IPayload
	{

		[SerializeField]
		public string value;

		public T Get<T>() => value is T f ? f : default;

		public void Set<T>(T input) => value = input is string v ? v : default;

	}
	
	[Serializable]
	public class Vector2Payload : IPayload
	{

		[SerializeField]
		public Vector2 value;

		public T Get<T>() => value is T f ? f : default;

		public void Set<T>(T input) => value = input is Vector2 v ? v : default;

	}

	[Serializable]
	public class Vector3Payload : IPayload
	{

		[SerializeField]
		public Vector3 value;

		public T Get<T>() => value is T f ? f : default;

		public void Set<T>(T input) => value = input is Vector3 v ? v : default;

	}
	
	[Serializable]
	public class MonoBehaviourPayload : IPayload
	{

		[SerializeReference]
		public MonoBehaviour value;

		public T Get<T>() => value is T f ? f : default;

		public void Set<T>(T input) => value = input is MonoBehaviour v ? v : default;

	}

}
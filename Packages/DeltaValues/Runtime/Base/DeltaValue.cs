using System;

using UnityEngine;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public abstract class DeltaValue<T> : ISerializationCallbackReceiver, IDisposable
	{

		[SerializeField]
		private string _Name;
		public string Name
		{
			get => _Name;
			set => _Name = value;
		}

		[SerializeField]
		public T _Value;
		public virtual T Value
		{
			get => _Value;
			set
			{
				if (ValueEquals(a: _Value,
				                b: value)) return;

				var old = _Value;

				_Value = value;
				
				#if debug
				Debug.Log($"[{Name}] changed from [{old}] to [{value}]");
				#endif

				OnValueChanged?.Invoke(arg1: old,
				                       arg2: value);
			}
		}

		[field: NonSerialized]
		public Action<T, T> OnValueChanged
		{
			get;
			set;
		}

		public abstract bool ValueEquals(T a,
		                                 T b);

		public virtual void OnBeforeSerialize() { }

		public virtual void OnAfterDeserialize() { }


		public DeltaValue() => 
			Name = $"Unknown {GetType().Name}";


		public DeltaValue(string name) => 
			Name  = !string.IsNullOrEmpty(name) ? name : $"Unknown {GetType().Name}";


		public DeltaValue(string name, T defaultValue)
		{
			Name  = !string.IsNullOrEmpty(name) ? name : $"Unknown {GetType().Name}";
			Value = defaultValue;
		}


		public virtual void Dispose() { }

	}

}
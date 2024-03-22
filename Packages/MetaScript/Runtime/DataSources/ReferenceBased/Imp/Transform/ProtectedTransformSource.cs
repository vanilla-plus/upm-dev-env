using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{

	[Serializable]
	public class ProtectedTransformSource : IProtectedComponentSource<Transform, ITransformSource>, ITransformSource
	{

		[SerializeField]
		private Transform _value;
		public Transform Value
		{
			get => _value;
			set
			{
				if (ReferenceEquals(_value,
				                    value)) return;
				
				var old = _value;
                
				_value = value;
                
				OnSet?.Invoke(_value);
				OnSetWithHistory?.Invoke(_value, old);
			}
		}

		[NonSerialized]
		private Action<Transform> _onSet;
		public Action<Transform> OnSet
		{
			get => _onSet;
			set => _onSet = value;
		}

		[NonSerialized]
		private Action<Transform,Transform> _onSetWithHistory;
		public Action<Transform,Transform> OnSetWithHistory
		{
			get => _onSetWithHistory;
			set => _onSetWithHistory = value;
		}

		public void OnBeforeSerialize() { }

		public void OnAfterDeserialize() { }

	}

}
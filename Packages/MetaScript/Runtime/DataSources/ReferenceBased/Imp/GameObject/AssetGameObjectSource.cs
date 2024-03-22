using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.MetaScript.DataSources.GameObject;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class AssetGameObjectSource : IGameObjectSource
	{

		[SerializeField]
		private GameObject _value;
		public GameObject Value
		{
			get => _value;
			set
			{
				if (ReferenceEquals(_value,
				                    value)) return;
				
				var old = _value;

				_value = value;

				OnSet?.Invoke(obj: _value);

				OnSetWithHistory?.Invoke(arg1: _value,
				                         arg2: old);
			}
		}

		[NonSerialized]
		private Action<GameObject> _onSet;
		public Action<GameObject> OnSet
		{
			get => _onSet;
			set => _onSet = value;
		}

		[NonSerialized]
		private Action<GameObject, GameObject> _onSetWithHistory;
		public Action<GameObject, GameObject> OnSetWithHistory
		{
			get => _onSetWithHistory;
			set => _onSetWithHistory = value;
		}

		public void OnBeforeSerialize() { }

		public void OnAfterDeserialize() { }

	}

}
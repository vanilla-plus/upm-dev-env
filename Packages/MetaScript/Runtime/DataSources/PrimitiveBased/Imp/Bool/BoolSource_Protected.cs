using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{

	[Serializable]
	public class BoolSource_Protected : IGettableSource<bool>,
	                                    ISettableSource<bool>,
	                                    IObservableSource<bool>
	{

		[SerializeField]
		private bool _value = false;
		public bool Value
		{
			get => _value;
			set
			{
				if (_value == value) return;
				
				_value = value;

				if (_value)
				{
					OnTrue?.Invoke();
				}
				else
				{
					OnFalse?.Invoke();
				}

				OnSet?.Invoke(_value);

				OnSetWithHistory?.Invoke(_value,
				                         !_value);
			}
		}

		[NonSerialized]
		private Action<bool> _onSet;
		public Action<bool> OnSet
		{
			get => _onSet;
			set => _onSet = value;
		}

		[NonSerialized]
		private Action<bool, bool> _onSetWithHistory;
		public Action<bool, bool> OnSetWithHistory
		{
			get => _onSetWithHistory;
			set => _onSetWithHistory = value;
		}

		[NonSerialized]
		private Action _onTrue;
		public Action OnTrue
		{
			get => _onTrue;
			set => _onTrue = value;
		}

		[NonSerialized]
		private Action _onFalse;
		public Action OnFalse
		{
			get => _onFalse;
			set => _onFalse = value;
		}


		public void Set(bool value)
		{
			if (_value == value) return;
				
			_value = value;

			if (_value)
			{
				OnTrue?.Invoke();
			}
			else
			{
				OnFalse?.Invoke();
			}

			OnSet?.Invoke(_value);

			OnSetWithHistory?.Invoke(_value,
			                         !_value);
		}

	}

}
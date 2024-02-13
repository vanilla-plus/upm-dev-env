using System;

using UnityEngine;

namespace Vanilla.DataSources
{
	
	[Serializable]
	public class ProtectedColorSource : ColorSource, IProtectedSource<Color>
	{

		[SerializeField]
		private Color _value;
		public override Color Value
		{
			get => _value;
			set
			{
				if (_value == value) return;
                
				var old = _value;

				_value = value;
                
				OnSet?.Invoke(_value);
				OnSetWithHistory?.Invoke(_value, old);
			}
		}

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }

	}
}
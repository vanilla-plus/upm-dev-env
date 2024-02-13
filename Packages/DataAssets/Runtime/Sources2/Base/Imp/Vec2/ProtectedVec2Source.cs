using System;

using UnityEngine;

namespace Vanilla.DataSources
{
	
	[Serializable]
	public class ProtectedVec2Source : Vec2Source, IProtectedSource<Vector2>
	{

		[SerializeField]
		private Vector2 _value;
		public override Vector2 Value
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
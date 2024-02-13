using System;

using UnityEngine;

namespace Vanilla.DataSources
{
	
	[Serializable]
	public class ProtectedVec4Source : Vec4Source, IProtectedSource<Vector4>
	{

		[SerializeField]
		private Vector4 _value;
		public override Vector4 Value
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
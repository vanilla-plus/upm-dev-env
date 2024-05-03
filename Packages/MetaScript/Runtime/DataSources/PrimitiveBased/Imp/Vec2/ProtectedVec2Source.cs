using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
	
	[Serializable]
	public class ProtectedVec2Source : Vec2Source, 
	                                   IProtectedSource<Vector2>
	{



		[SerializeField]
		private Vector2 _value;
		public override Vector2 Value
		{
			get => _value;
			set
			{
				if (_value == value) return;
                
				var outgoing = _value;

				_value = value;

				OnSet?.Invoke(value);
				OnSetWithHistory?.Invoke(value, outgoing);
			}
		}

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }

	}
}
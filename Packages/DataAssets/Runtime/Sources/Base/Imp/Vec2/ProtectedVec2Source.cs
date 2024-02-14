using System;

using UnityEngine;

namespace Vanilla.DataSources
{
	
	[Serializable]
	public class ProtectedVec2Source : Vec2Source, 
	                                   IProtectedSource<Vector2>
	{

		[SerializeField]
		private string _name = "Unnamed ProtectedRangedColorSource";
		public string Name
		{
			get => _name;
			set => _name = value;
		}



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

				#if debug
				Debug.Log($"[{Name}] was changed from [{outgoing}] to [{value}]");
				#endif
                
				OnSet?.Invoke(value);
				OnSetWithHistory?.Invoke(value, outgoing);
			}
		}

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }

	}
}
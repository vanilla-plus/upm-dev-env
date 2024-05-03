using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
	
	[Serializable]
	public class ProtectedVec3Source : Vec3Source,
	                                   IProtectedSource<Vector3>
	{

		[SerializeField]
		private Vector3 _value;
		public override Vector3 Value
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
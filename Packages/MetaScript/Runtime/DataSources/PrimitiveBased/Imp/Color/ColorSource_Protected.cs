using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
	
	[Serializable]
	public class ColorSource_Protected : ColorSource_Observable, 
	                                     IProtectedSource<Color>
	{
		
		[SerializeField]
		private Color _value;
		public override Color Value
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
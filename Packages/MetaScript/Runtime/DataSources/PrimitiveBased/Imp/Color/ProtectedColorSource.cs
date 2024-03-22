using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
	
	[Serializable]
	public class ProtectedColorSource : ColorSource, 
	                                    IProtectedSource<Color>
	{

		[SerializeField]
		private string _name = "Unnamed ProtectedBoolSource";
		public string Name
		{
			get => _name;
			set => _name = value;
		}




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
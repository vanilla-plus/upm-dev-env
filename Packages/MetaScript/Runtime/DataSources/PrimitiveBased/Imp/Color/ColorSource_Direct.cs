using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{

	[Serializable]
	public class ColorSource_Direct : ColorSource
	{
        
		[SerializeField] private Color _value;
		public override Color Value
		{
			get => _value;
			set => _value = value;
		}

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }


	}

}
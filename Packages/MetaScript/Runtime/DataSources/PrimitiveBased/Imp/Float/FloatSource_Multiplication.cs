using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
    
	[Serializable]
	public class FloatSource_Multiplication : FloatSource
	{

		[SerializeReference] public FloatSource A;
		[SerializeReference] public FloatSource B;
        
		public override float Value
		{
			get => A.Value * B.Value;
			set { }
		}

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }

	}
}
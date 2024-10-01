using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{

	[Serializable]
	public class FloatSource_Vec3_Magnitude : FloatSource
	{

		[SerializeReference] public Vec3Source A;

		public override float Value
		{
			get => A.Value.magnitude;
			set { }
		}

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }

	}

}
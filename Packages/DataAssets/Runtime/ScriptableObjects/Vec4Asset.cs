using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Vec4 Asset",
	                 menuName = "Vanilla/Data Assets/Vec4",
	                 order = 6)]
	public class Vec4Asset : DataAsset<Vector4>
	{

		[SerializeField]
		private DeltaVec4 _delta = new();
		public override DeltaValue<Vector4> Delta
		{
			get => _delta;
			set => _delta = value as DeltaVec4;
		}

	}

}
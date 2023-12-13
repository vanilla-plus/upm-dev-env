using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Vec1 Asset",
	                 menuName = "Vanilla/Data Assets/Vec3",
	                 order = 4)]
	public class Vec3Asset : DataAsset<Vector3>
	{

		[SerializeField]
		private DeltaVec3 _delta = new();
		public override DeltaValue<Vector3> Delta
		{
			get => _delta;
			set => _delta = value as DeltaVec3;
		}

	}

}
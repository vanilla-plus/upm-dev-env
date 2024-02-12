using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Float Asset",
	                 menuName = "Vanilla/Data Assets/Vec3",
	                 order = 5)]
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
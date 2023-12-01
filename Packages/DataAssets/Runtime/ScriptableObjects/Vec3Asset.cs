using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Vec3 Asset",
	                 menuName = "Vanilla/Data Assets/Vec3",
	                 order = 4)]
	public class Vec3Asset : ScriptableObject
	{

		[SerializeField]
		private DeltaVec3 deltaValue = new DeltaVec3();
		public DeltaVec3 DeltaValue => deltaValue;

	}

}
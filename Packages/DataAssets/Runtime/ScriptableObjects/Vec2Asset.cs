using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Vec2 Asset",
	                 menuName = "Vanilla/Data Assets/Vec2",
	                 order = 3)]
	public class Vec2Asset : ScriptableObject
	{

		[SerializeField]
		private DeltaVec2 deltaValue = new DeltaVec2();
		public DeltaVec2 DeltaValue => deltaValue;

	}

}
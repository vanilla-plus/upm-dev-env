using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Vec1 Asset",
	                 menuName = "Vanilla/Data Assets/Vec1",
	                 order = 2)]
	public class Vec1Asset : ScriptableObject
	{

		[SerializeField]
		private DeltaVec1 deltaValue = new DeltaVec1();
		public DeltaVec1 DeltaValue => deltaValue;

	}

}
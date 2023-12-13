using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Vec1 Asset",
	                 menuName = "Vanilla/Data Assets/Vec1",
	                 order = 2)]
	public class Vec1Asset : DataAsset<float>
	{

		[SerializeField]
		private DeltaVec1 _delta = new();
		public override DeltaValue<float> Delta
		{
			get => _delta;
			set => _delta = value as DeltaVec1;
		}

	}

}
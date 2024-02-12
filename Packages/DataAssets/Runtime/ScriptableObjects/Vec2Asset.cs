using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Vec2 Asset",
	                 menuName = "Vanilla/Data Assets/Vec2",
	                 order = 4)]
	public class Vec2Asset : DataAsset<Vector2>
	{

		[SerializeField]
		private DeltaVec2 _delta = new();
		public override DeltaValue<Vector2> Delta
		{
			get => _delta;
			set => _delta = value as DeltaVec2;
		}

	}

}
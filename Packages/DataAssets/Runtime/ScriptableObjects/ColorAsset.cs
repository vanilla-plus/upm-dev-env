using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Color Asset",
	                 menuName = "Vanilla/Data Assets/Color",
	                 order = 7)]
	public class ColorAsset : DataAsset<Color>
	{

		[SerializeField]
		private DeltaColor _delta = new();
		public override DeltaValue<Color> Delta
		{
			get => _delta;
			set => _delta = value as DeltaColor;
		}

	}

}
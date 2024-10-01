using System;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Color Asset",
	                 menuName = "Vanilla/MetaScript/Data Assets/Color",
	                 order = 7)]
	public class ColorAsset : DataAsset<Color,ColorSource_Observable>
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		private ColorSource_Observable _source;
		public override ColorSource_Observable Source
		{
			get => _source;
			set => _source = value;
		}

	}

}
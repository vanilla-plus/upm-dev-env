using System;

using UnityEngine;

using Vanilla.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Color Asset",
	                 menuName = "Vanilla/Data Assets/Color",
	                 order = 7)]
	public class ColorAsset : DataAsset<Color>
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		private ColorSource _source;
		public override IDataSource<Color> Source
		{
			get => _source;
			set => _source = value as ColorSource;
		}

	}

}
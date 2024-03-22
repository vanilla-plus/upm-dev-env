using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Color Asset",
	                 menuName = "Vanilla/Data Assets/Color",
	                 order = 7)]
	public class ColorAsset : DataAsset<Color,ColorSource>
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		private ColorSource _source;
		public override ColorSource Source
		{
			get => _source;
			set => _source = value;
		}

	}

}
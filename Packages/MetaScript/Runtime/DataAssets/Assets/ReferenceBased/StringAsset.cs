using System;

using UnityEngine;

using Vanilla.TypeMenu;

using Vanilla.MetaScript.DataSources.Strings;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "String Asset",
	                 menuName = "Vanilla/MetaScript/Data Assets/String",
	                 order = 7)]
	public class StringAsset : DataAsset<string,StringSource>
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		private StringSource _source;
		public override StringSource Source
		{
			get => _source;
			set => _source = value;
		}

	}

}
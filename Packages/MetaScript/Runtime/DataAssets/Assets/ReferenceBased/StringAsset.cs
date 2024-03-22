using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "String Asset",
	                 menuName = "Vanilla/Data Assets/String",
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
using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Bool Asset",
	                 menuName = "Vanilla/Data Assets/Bool",
	                 order = 0)]
	public class BoolAsset : DataAsset<bool, BoolSource>
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		private BoolSource _source;
		public override BoolSource Source
		{
			get => _source;
			set => _source = value;
		}

	}

}
using System;

using UnityEngine;

using Vanilla.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Bool Asset",
	                 menuName = "Vanilla/Data Assets/Bool",
	                 order = 0)]
	public class BoolAsset : DataAsset<bool>
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		private BoolSource _source;
		public override IDataSource<bool> Source
		{
			get => _source;
			set => _source = value as BoolSource;
		}

	}

}
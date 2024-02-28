using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Int Asset",
	                 menuName = "Vanilla/Data Assets/Int",
	                 order = 1)]
	public class IntAsset : DataAsset<int>
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		private IntSource _source;
		public override IDataSource<int> Source
		{
			get => _source;
			set => _source = value as IntSource;
		}
		
	}

}
using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Vec2 Asset",
	                 menuName = "Vanilla/Data Assets/Vec2",
	                 order = 4)]
	public class Vec2Asset : DataAsset<Vector2>
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		private Vec2Source _source;
		public override IDataSource<Vector2> Source
		{
			get => _source;
			set => _source = value as Vec2Source;
		}

	}

}
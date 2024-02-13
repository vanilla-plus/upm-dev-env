using System;

using UnityEngine;

using Vanilla.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Vec4 Asset",
	                 menuName = "Vanilla/Data Assets/Vec4",
	                 order = 6)]
	public class Vec4Asset : DataAsset<Vector4>
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		private Vec4Source _source;
		public override IDataSource<Vector4> Source
		{
			get => _source;
			set => _source = value as Vec4Source;
		}

	}

}
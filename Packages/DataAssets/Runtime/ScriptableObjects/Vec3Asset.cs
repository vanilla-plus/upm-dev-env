using System;

using UnityEngine;

using Vanilla.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Float Asset",
	                 menuName = "Vanilla/Data Assets/Vec3",
	                 order = 5)]
	public class Vec3Asset : DataAsset<Vector3>
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		private Vec3Source _source;
		public override IDataSource<Vector3> Source
		{
			get => _source;
			set => _source = value as Vec3Source;
		}

	}

}
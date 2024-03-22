using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Float Asset",
	                 menuName = "Vanilla/Data Assets/Vec3",
	                 order = 5)]
	public class Vec3Asset : DataAsset<Vector3,Vec3Source>
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		private Vec3Source _source;
		public override Vec3Source Source
		{
			get => _source;
			set => _source = value;
		}

	}

}
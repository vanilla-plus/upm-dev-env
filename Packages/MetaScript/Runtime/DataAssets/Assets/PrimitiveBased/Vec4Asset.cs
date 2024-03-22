using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Vec4 Asset",
	                 menuName = "Vanilla/Data Assets/Vec4",
	                 order = 6)]
	public class Vec4Asset : DataAsset<Vector4,Vec4Source>
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		private Vec4Source _source;
		public override Vec4Source Source
		{
			get => _source;
			set => _source = value;
		}

	}

}
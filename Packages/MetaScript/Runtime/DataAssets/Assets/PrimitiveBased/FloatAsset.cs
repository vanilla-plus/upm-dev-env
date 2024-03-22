using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Float Asset",
	                 menuName = "Vanilla/Data Assets/Float",
	                 order = 3)]
	public class FloatAsset : DataAsset<float,FloatSource>
	{

		[SerializeReference]
		[TypeMenu("yellow")]
		private FloatSource _source;
		public override FloatSource Source
		{
			get => _source;
			set => _source = value;
		}

	}

}
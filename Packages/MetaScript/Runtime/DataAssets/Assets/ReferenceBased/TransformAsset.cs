using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Transform Asset",
	                 menuName = "Vanilla/Data Assets/Transform",
	                 order = 9)]
	public class TransformAsset : ComponentAsset<Transform, ITransformSource>
	{

		[SerializeReference]
		[TypeMenu("red")]
		private ITransformSource _source;
		public override ITransformSource Source
		{
			get => _source;
			set => _source = value;
		}

	}

}
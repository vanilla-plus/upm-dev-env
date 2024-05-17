using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources.GenericComponent;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Animator Asset",
	                 menuName = "Vanilla/MetaScript/Data Assets/Animator",
	                 order = 10)]
	public class AnimatorAsset : ComponentAsset<Animator, AnimatorSource>
	{

		[SerializeReference]
		[TypeMenu("red")]
		private AnimatorSource _source;
		public override AnimatorSource Source
		{
			get => _source;
			set => _source = value;
		}

		public override void Assign(GameObject g) => base.Assign(g);

	}

}
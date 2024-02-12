using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Bool Asset",
	                 menuName = "Vanilla/Data Assets/Bool",
	                 order = 0)]
	public class BoolAsset : DataAsset<bool>
	{

		[SerializeField]
		private DeltaBool _delta = new();
		public override DeltaValue<bool> Delta
		{
			get => _delta;
			set => _delta = value as DeltaBool;
		}

	}

}
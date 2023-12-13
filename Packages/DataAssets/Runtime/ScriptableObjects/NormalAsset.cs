using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Normal Asset",
	                 menuName = "Vanilla/Data Assets/Normal",
	                 order = 2)]
	public class NormalAsset : DataAsset<float>
	{

		[SerializeField]
		public DeltaNormal _delta = new();
		public override DeltaValue<float> Delta
		{
			get => _delta;
			set => _delta = value as DeltaNormal;
		}

	}

}
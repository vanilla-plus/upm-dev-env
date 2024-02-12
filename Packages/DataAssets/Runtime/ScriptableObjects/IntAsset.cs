using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Int Asset",
	                 menuName = "Vanilla/Data Assets/Int",
	                 order = 1)]
	public class IntAsset : DataAsset<int>
	{

		[SerializeField]
		private DeltaInt _delta = new();
		public override DeltaValue<int> Delta
		{
			get => _delta;
			set => _delta = value as DeltaInt;
		}
		
	}

}
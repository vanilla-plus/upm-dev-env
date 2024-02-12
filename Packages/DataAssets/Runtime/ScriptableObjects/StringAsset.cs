using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "String Asset",
	                 menuName = "Vanilla/Data Assets/String",
	                 order = 7)]
	public class StringAsset : DataAsset<string>
	{

		[SerializeField]
		private DeltaString _delta = new();
		public override DeltaValue<string> Delta
		{
			get => _delta;
			set => _delta = value as DeltaString;
		}

	}

}
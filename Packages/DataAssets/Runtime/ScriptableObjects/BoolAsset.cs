using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Bool Asset",
	                 menuName = "Vanilla/Data Assets/Bool",
	                 order = 1)]
	public class BoolAsset : ScriptableObject
	{

		[SerializeField]
		private DeltaBool deltaValue = new DeltaBool();
		public DeltaBool DeltaValue => deltaValue;

	}

}
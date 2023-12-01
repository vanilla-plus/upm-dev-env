using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Int Asset",
	                 menuName = "Vanilla/Data Assets/Int",
	                 order = 4)]
	public class IntAsset : ScriptableObject
	{

		[SerializeField]
		private DeltaInt deltaValue = new DeltaInt(defaultName: "Unknown DeltaInt",
		                                           defaultValue: 0,
		                                           defaultMin: int.MinValue,
		                                           defaultMax: int.MaxValue);
		public DeltaInt DeltaValue => deltaValue;

	}

}
using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "Float Asset",
	                 menuName = "Vanilla/Data Assets/Float",
	                 order = 3)]
	public class FloatAsset : DataAsset<float>
	{

		[SerializeField]
		private DeltaFloat _delta = new();
		public override DeltaValue<float> Delta
		{
			get => _delta;
			set => _delta = value as DeltaFloat;
		}

	}

}
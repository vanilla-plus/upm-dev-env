using System;

using UnityEngine;

namespace Vanilla.Roulette
{

	[Serializable]
	public class RouletteTableItem : IRouletteItem
	{

		[SerializeField]
		private float _probability = 1.0f;
		public float Probability
		{
			get => _probability;
			set => _probability = value;
		}

		public RouletteTableItem(float probability = 1.0f) => _probability = probability;

	}

}
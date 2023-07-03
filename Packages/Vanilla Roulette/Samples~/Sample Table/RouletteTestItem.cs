using System;

using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Roulette
{

	[Serializable]
	public class RouletteTestItem : IRouletteItem
	{

		[SerializeField]
		private float _probability = 0.5f;
		public float Probability
		{
			get => _probability;
			set => _probability = value;
		}

		[SerializeField]
		public string message;

	}

}
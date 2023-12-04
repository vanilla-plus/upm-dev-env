#if vanilla_type_menu
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Vanilla.TypeMenu;

using Random = UnityEngine.Random;

namespace Vanilla.Roulette
{

	[Serializable]
	public class PolymorphicTable
	{

        [SerializeReference]
        [TypeMenu("blue")]
        public List<IRouletteItem> items = new();

		public T SpinForAnItem<T>() where T : IRouletteItem
		{
			var totalProbability = items.Sum(c => c.Probability);

			var randomValue = Random.Range(0f,
			                               totalProbability);

			var compoundProbability = 0.0f;
            
			foreach (var i in items)
			{
				compoundProbability += i.Probability;
                
				if (randomValue <= compoundProbability) return (T)i;
			}

			return default;
		}
        
		public int SpinForAnIndex()
		{
			var totalProbability = items.Sum(c => c.Probability);

			var randomValue = Random.Range(0f,
			                               totalProbability);

			var compoundProbability = 0.0f;

			for (var i = 0;
			     i < items.Count;
			     i++)
			{
				compoundProbability += items[i].Probability;

				if (randomValue <= compoundProbability) return i;
			}

			return default;
		}

	}

}
#endif
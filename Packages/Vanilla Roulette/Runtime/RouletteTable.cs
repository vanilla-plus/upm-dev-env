using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Random = UnityEngine.Random;

namespace Vanilla.Roulette
{

    [Serializable]
    public class RouletteTable<T> : ISerializationCallbackReceiver
        where T :  IRouletteItem
    {

        [SerializeField]
        private float totalProbability;

        [SerializeField]
        public List<T> items = new();

        public void UpdateTotalPercentages() => totalProbability = items.Sum(c => c.Probability);
        
        public T SpinForAnItem()
        {
            var randomValue = Random.Range(0f,
                                           totalProbability);

            var compoundProbability = 0.0f;
            
            foreach (var i in items)
            {
                compoundProbability += i.Probability;
                
                if (randomValue <= compoundProbability) return i;
            }

            return default;
        }
        
        public int SpinForAnIndex()
        {
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
        
        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            #if UNITY_EDITOR
            if (items is
                {
                    Count: > 0
                } && items.All(i => i != null))
            {
                UpdateTotalPercentages();
            }
            #endif
        }

    }

}

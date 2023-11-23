using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.Drivers
{
    
    [Serializable]
    public class DriverInstance : MonoBehaviour
    {

        [NonSerialized]
        public Dictionary<string, DriverSet> dictionary = new Dictionary<string, DriverSet>();

        [SerializeField]
        public DriverSet[] sets = Array.Empty<DriverSet>();


        private void OnValidate()
        {
            #if UNITY_EDITOR
            // This bulky duplicate check serves two sad-but-necessary purposes.
            // First, it eliminates a bug in the Unity Editor where new instances in the array share the same object reference in memory,
            // causing edits to one entry to affect others.
            // Second, it somewhat ensures that PropertyDriverSets have a unique name (at least within their MaterialDriver instance).

            var duplicates = sets.GroupBy(x => x.Name)
                                 .Where(g => g.Count() > 1)
                                 .SelectMany(g => g.Skip(1))
                                 .ToArray();

            if (duplicates.Length > 0)
            {
                Debug.LogWarning("Duplicate Name detected! These names need to be unique as they act as a dictionary key.");

                foreach (var t in duplicates)
                {
                    for (var propId = 0;
                         propId < t.drivers.Length;
                         propId++)
                    {
                        t.drivers[propId] = null;
                    }
                }
            }
            
            foreach (var set in sets)
            {
                set.OnValidate();
            }
            #endif
        }


        void Awake() => RebuildDictionary();


        public void RebuildDictionary()
        {
            dictionary.Clear();

            foreach (var set in sets)
                dictionary.Add(key: set.Name,
                               value: set);
        }

        void OnEnable()
        {
            foreach (var set in sets) set.Init();
        }
        
        void OnDisable()
        {
            foreach (var set in sets) set.DeInit();
        }

    }

    [Serializable]
    public class DriverSet
    {

        [SerializeField]
        public string Name;
        
        [SerializeField]
        public Normal normal = new Normal();

        [SerializeReference]
        [TypeMenu]
        public IDriver[] drivers = Array.Empty<IDriver>();


        public void OnValidate()
        {
            #if UNITY_EDITOR
            normal.OnValidate();
            
            foreach (var d in drivers) d?.OnValidate(driverSet: this);
            #endif
        }


        public void Init()
        {
            foreach (var d in drivers) d?.Init(driverSet: this);
        }


        public void DeInit()
        {
            foreach (var d in drivers) d?.DeInit(driverSet: this);
        }

    }
    
}
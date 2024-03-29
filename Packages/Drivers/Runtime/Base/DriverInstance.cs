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
        public static Dictionary<string, DriverSet> GlobalDictionary = new Dictionary<string, DriverSet>();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void StaticReset()
        {
            #if UNITY_EDITOR
            GlobalDictionary.Clear();
            #endif
        }
        
        [SerializeField]
        public bool Global = false;
        
        [NonSerialized]
        public Dictionary<string, DriverSet> LocalDictionary = new Dictionary<string, DriverSet>();

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

        public void AddToDictionary()
        {
            if (Global)
            {
                foreach (var set in sets)
                    GlobalDictionary.Add(key: set.Name,
                                         value: set);
            }
            else
            {
                LocalDictionary.Clear();

                foreach (var set in sets)
                    LocalDictionary.Add(key: set.Name,
                                        value: set);
            }
        }


        public void RemoveFromDictionary()
        {
            if (Global)
            {
                foreach (var set in sets) 
                    GlobalDictionary.Remove(set.Name);
            }
            else
            {
                LocalDictionary.Clear();
            }
        }

        void OnEnable()
        {
            AddToDictionary();
            
            foreach (var set in sets) set.Init();
        }
        
        void OnDisable()
        {
            foreach (var set in sets) set.DeInit();
            
            RemoveFromDictionary();
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
        [TypeMenu("blue")]
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
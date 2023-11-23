using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Vanilla.MaterialDriver
{

    
    [Serializable]
    public class MaterialDriver : MonoBehaviour
    {

        [NonSerialized]
        public static Dictionary<string, PropertyDriverSet> SetDictionary = new Dictionary<string, PropertyDriverSet>();
        
        [SerializeField]
        public PropertyDriverSet[] PropertySets = Array.Empty<PropertyDriverSet>();


        void OnValidate()
        {
            #if UNITY_EDITOR
            
            // This bulky duplicate check serves two sad-but-necessary purposes.
            // First, it eliminates a bug in the Unity Editor where new instances in the array share the same object reference in memory,
            // causing edits to one entry to affect others.
            // Second, it somewhat ensures that PropertyDriverSets have a unique name (at least within their MaterialDriver instance).
            
            var duplicates = PropertySets.GroupBy(x => x.Name)
                                         .Where(g => g.Count() > 1)
                                         .SelectMany(g => g.Skip(1))
                                         .ToArray();

            if (duplicates.Length > 0)
            {
                Debug.LogWarning("Duplicate PropertyDriverSet Name detected! These names need to be unique as they act as a static dictionary key.");

                foreach (var t in duplicates)
                {
                    for (var propId = 0;
                         propId < t.Properties.Length;
                         propId++)
                    {
                        t.Properties[propId] = null;
                    }
                }
            }

            foreach (var p in PropertySets)
            {
                p?.OnValidate();
            }
            #endif
        }


        void Awake()
        {
            foreach (var p in PropertySets)
            {
                if (p == null) continue;

                SetDictionary.Add(key: p.Name,
                                  value: p);

                p.Init();
            }
        }
        
        void OnDestroy()
        {
            foreach (var p in PropertySets)
            {
                SetDictionary.Remove(key: p.Name);

                p.DeInit();
            }
        }

    }

}
using System;
using System.Collections.Generic;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.Drivers
{
    
    [Serializable]
    public class DriverInstance : MonoBehaviour
    {

        [SerializeField]
        public bool DebugMode = false;
        
        [NonSerialized]
        public Dictionary<string, DriverSet> dictionary = new Dictionary<string, DriverSet>();

        [SerializeField]
        public DriverSet[] sets = Array.Empty<DriverSet>();


        private void OnValidate()
        {
            #if UNITY_EDITOR
            foreach (var set in sets)
            {
                set.OnValidate(this);
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


        public void OnValidate(DriverInstance instance)
        {
            #if UNITY_EDITOR
            foreach (var d in drivers)
                d?.OnValidate(instance,
                              this);

//            foreach (var d in drivers) d?.Interpolate(normal: normal.Value);
            #endif
        }


        public void Init()
        {
            foreach (var d in drivers) d?.Init(this);
        }


        public void DeInit()
        {
            foreach (var d in drivers) d?.DeInit(this);
        }

    }
    
}
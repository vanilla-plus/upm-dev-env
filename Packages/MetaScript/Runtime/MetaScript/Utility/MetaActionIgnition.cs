using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.Init;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class MetaActionIgnition : MonoBehaviour, IInitiable
    {

        [SerializeField]
        public MetaAction action;

        [SerializeReference]
        [TypeMenu("green")]
        public IScopeSource scopeSource;

        [SerializeReference]
        public MetaTaskInstance target;


        void OnValidate()
        {
            #if UNITY_EDITOR
            if (target == null) target = GetComponent<MetaTaskInstance>();
            #endif
        }
        
        public void Init() => action.OnInvoke += Ignite;

        public void PostInit() { }


        public void Start()
        {
            // Is this a bad idea..?
            if (action == null) Ignite();
        }
        
        void OnDestroy() => action.OnInvoke -= Ignite;
        
        public void Ignite()
        {
            var scope = scopeSource?.CreateScope(null);
            
            target?.Task?.Run(scope);
        }
        
    }
}

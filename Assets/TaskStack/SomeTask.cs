using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace MagicalProject
{
    
    [Serializable]
    public abstract class SomeTask
    {

        [SerializeField]
        public string Name;


        public virtual void OnValidate()
        {
            #if UNITY_EDITOR
            Name = CanAutoName ? AutoName : "???";
            #endif
        }


        protected virtual bool   CanAutoName => true;

        protected abstract string AutoName
        {
            get;
        }
        
        public abstract    UniTask<Scope> Run(Scope scope);

    }

}

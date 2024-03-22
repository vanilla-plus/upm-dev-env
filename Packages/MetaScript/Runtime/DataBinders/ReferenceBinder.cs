using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataBinders
{
    
    [Serializable]
    public abstract class ReferenceBinder<T,S,A> : MonoBehaviour
        where T : class
        where S : class, IRefSource<T,S>
        where A : RefAsset<T,S>
    {

        [SerializeField]
        public A Asset;

        public void OnEnable() => Asset.Source.Value = Assign;
        public void OnDisable() => Asset.Source.Value = null;

        protected abstract T Assign
        {
            get;
        }
        
        

    }
}

using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Pools
{
    
    [Serializable]
    public class PoolItemTest : MonoBehaviour, IPoolItem
    {
       
        [NonSerialized] 
        private IPool _pool;
        public IPool Pool
        {
            get => _pool;
            set => _pool = value;
        }

        public void OnEnable() => Debug.Log("Activated!");
        
        public void OnDisable() => Debug.Log("Deactivated, resetting myself");

//        public void RetireSelf() => this.Retire();

    }
}

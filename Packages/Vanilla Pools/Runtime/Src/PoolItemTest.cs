using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Pools
{
    
    [Serializable]
    public class PoolItemTest : MonoBehaviour, IPoolItem<TestPool, PoolItemTest>
    {

        [SerializeField] private bool _leased;
        public bool Leased
        {
            get => _leased;
            set => _leased = value;
        }

        [NonSerialized] 
        private TestPool _pool;
        public TestPool Pool
        {
            get => _pool;
            set => _pool = value;
        }

        public void HandleGet()    => Debug.Log("Argh, somebody got me!");
        
        public void HandleRetire() => Debug.Log("Argh, somebody... retired me!");

    }
}

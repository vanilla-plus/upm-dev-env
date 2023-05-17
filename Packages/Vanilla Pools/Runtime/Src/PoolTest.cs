//using System;
//using System.Collections;
//using System.Collections.Generic;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//
//using Object = UnityEngine.Object;
//

using System;

using UnityEngine;

namespace Vanilla.Pools
{

//
//
    using Vanilla.Pools;

//
//    [Serializable]
//    public class TestStockAsyncPool : IPrefabStockAsyncPool<TestStockAsyncPool,PoolItemTest>
//    {
//
//
//
//    }
////
    [Serializable]
    public class PoolTest : MonoBehaviour
    {

        [SerializeField]
        public TestPool Pool = new TestPool();

        [SerializeField]
        public PoolItemTest current;


        [ContextMenu("Fill")]
        public void Fill() => Pool.CreateAll();


        [ContextMenu("Get All")]
        public async void GetAll()
        {
//            while (Pool.Inactive.TryPeek(out var item)) current = await Pool.Get();

            for (var i = 0;
                 i < Pool.Total;
                 i++)
            {
                current = Pool.Get();
            }
        }


        [ContextMenu("Retire All")]
        public void RetireAll() => Pool.RetireAll();


        [ContextMenu("Get")]
        public void TestGet() => current = Pool.Get();


        [ContextMenu("Retire")]
        public void TestRetire() => Pool.Retire(current);

    }

    [Serializable]
    public class TestPool : StockPool<TestPool, PoolItemTest> { }

}
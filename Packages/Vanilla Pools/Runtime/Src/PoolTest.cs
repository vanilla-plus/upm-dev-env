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

    [Serializable]
    public class PoolTest : MonoBehaviour
    {

        [SerializeField]
        public RingPool Pool = new ();

        [SerializeField]
        public PoolItemTest current;

        [ContextMenu("Fill")]
        public void Fill() => Pool.CreateAll();


        [ContextMenu("Get All")]
        public void GetAll()
        {
//            while (Pool.Inactive.TryPeek(out var item)) current = await Pool.Get();

            for (var i = 0;
                 i < Pool.Total;
                 i++)
            {
                current = Pool.Get() as PoolItemTest;
            }
            
            ((IPoolItem)current)?.Retire();
        }


        [ContextMenu("Retire All")]
        public void RetireAll() => Pool.RetireAll();


        [ContextMenu("Get")]
        public void TestGet() => current = Pool.Get() as PoolItemTest;


        [ContextMenu("Retire")]
        public void TestRetire() => Pool.Retire(current);

    }

}
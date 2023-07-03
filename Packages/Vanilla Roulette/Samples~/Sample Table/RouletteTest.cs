using System;

using UnityEngine;

namespace Vanilla.Roulette
{
    
    [Serializable]
    public class RouletteTest : MonoBehaviour
    {

        [SerializeField]
        public RouletteTable<RouletteTestItem> table = new();

        void Update()
        {
            if (Input.GetKey(KeyCode.Alpha0)) Debug.Log(table.Spin()?.message);
        }
        
    }
}

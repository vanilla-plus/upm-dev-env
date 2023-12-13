using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.Globals
{
    
    
    public class GlobalsInstance : MonoBehaviour
    {

        public static Dictionary<int, DeltaBool> Bool = new Dictionary<int, DeltaBool>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void StaticReset()
        {
            #if UNITY_EDITOR
            Bool.Clear();
            #endif
        }
        
    }
}

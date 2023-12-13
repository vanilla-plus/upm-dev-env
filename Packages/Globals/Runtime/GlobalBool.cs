using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.Globals
{
    
    [Serializable]
    public class GlobalBool : MonoBehaviour
    {

        [SerializeField]
        public string Name;

        [SerializeField]
        public DeltaBool Delta = new DeltaBool();

    }
}

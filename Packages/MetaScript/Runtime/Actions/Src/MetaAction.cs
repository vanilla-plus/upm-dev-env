using System;

using UnityEngine;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    [CreateAssetMenu(order = 0, fileName = "New MetaAction", menuName = "Vanilla/MetaScript/MetaActions/Event")]
    public class MetaAction : ScriptableObject
    {

        [NonSerialized] public Action OnInvoke;
        
        [ContextMenu("Debug Invoke")]
        public void Invoke() => OnInvoke?.Invoke();

    }
}

using System;

using UnityEngine;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    [CreateAssetMenu(order = 0, fileName = "New MetaAction", menuName = "Vanilla/MetaScript/MetaActions/Event")]
    public class MetaAction : MetaAction_Base
    {

        [NonSerialized] public Action OnInvoke;
        
        [ContextMenu("Debug Invoke")]
        public override void Invoke() => OnInvoke?.Invoke();

    }
}

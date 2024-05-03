using UnityEngine;

namespace Vanilla.MetaScript
{

    public abstract class MetaActionInvocation : MonoBehaviour
    {

        public MetaAction_Base target;
        
        [ContextMenu("Invoke")]
        public void Invoke() => target.Invoke();
        
    }

}
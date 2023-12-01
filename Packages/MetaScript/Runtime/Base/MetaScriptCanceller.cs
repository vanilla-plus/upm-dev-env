using System;

using UnityEngine;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class MetaScriptCanceller : MonoBehaviour
    {

        [SerializeField]
        public string scopeName;

        [SerializeField]
        public KeyCode debugCancelKey = KeyCode.None;
        
        void Update()
        {
            #if debug
            if (Input.GetKeyDown(debugCancelKey)) Cancel();
            #endif
        }

        [ContextMenu("Cancel")]
        public void Cancel() => Scope.TryCancel(scopeName);

    }
}

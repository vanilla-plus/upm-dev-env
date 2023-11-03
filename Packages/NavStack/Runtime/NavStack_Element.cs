using System;

using UnityEngine;

namespace Vanilla.NavStack
{
    
    [Serializable]
    public class NavStack_Element : MonoBehaviour
    {
        [SerializeField]
        protected NavStack _stack;

        protected virtual void OnValidate()
        {
            #if UNITY_EDITOR
            if (_stack == null)
            {
                _stack = GetComponentInParent<NavStack>();

                if (_stack == null)
                {
                    Debug.LogWarning($"NavStack_Element [{gameObject.name}] couldn't find a NavStack in any parent! (That's bad).");

                    return;
                }
            }
            #endif
        }
    }
}

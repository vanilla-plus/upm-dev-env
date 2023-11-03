using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.NavStack
{
    
    [RequireComponent(typeof(UI_Window))]
    public class NavStack_Window : NavStack_Element
    {

        [SerializeField]
        public UI_Window _window;

        [Tooltip("When this Window is open and still in the stack, should it remain open or be hidden?")]
        [SerializeField]
        public bool RemainOpenUnderneath = false;
        
        private void OnValidate()
        {
            #if UNITY_EDITOR
            base.OnValidate();
            
            _window = GetComponent<UI_Window>();

            if (_window)
            {
                _window.State.UseScaledTime = _stack.useScaledTime;
            }
            #endif
        }


//        void Awake() => _window.Active.Active.OnTrue += PushToStack;


//        void OnDestroy() => _window.Active.Active.OnTrue -= PushToStack;



        public void Nav_Open() => _stack.Nav_Open(this).Forget();

    }
}

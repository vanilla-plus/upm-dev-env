using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MetaScript
{

    public class InitMetaActionInvocation : MetaActionInvocation
    {

//        public void DebugInvoke() => I
        
        public enum WakeMethod
        {

            Awake,
            Start,
            Init,
            PostInit,
            OnEnable

        }

        [SerializeField]
        public WakeMethod wakeMethod = WakeMethod.Init;


        void Awake()
        {
            if (wakeMethod != WakeMethod.Awake) return;
            
            Invoke();
        }


        void Start()
        {
            if (wakeMethod != WakeMethod.Start) return;

            Invoke();
        }


        public void Init()
        {
            if (wakeMethod != WakeMethod.Init) return;

            Invoke();
        }


        public void PostInit()
        {
            if (wakeMethod != WakeMethod.PostInit) return;

            Invoke();
        }


        void OnEnable()
        {
            if (wakeMethod != WakeMethod.OnEnable) return;

            Invoke();
        }


        void OnDisable()
        {
            if (wakeMethod != WakeMethod.OnEnable) return;

            Invoke();
        }


        private void OnDestroy()
        {
            if (wakeMethod == WakeMethod.OnEnable) return;

            Invoke();
        }

    }

}
using System;

using UnityEngine;
using UnityEngine.UI;

namespace Vanilla.MetaScript.Debugger
{
    
    [Serializable]
    public class MetaScriptDebugger : MonoBehaviour
    {

        [SerializeField]
        public MetaTaskInstance instance;
        
        [SerializeField]
        public Text text;

        [NonSerialized] private Tracer tracer;

        void OnEnable()
        {
            if (!instance) return;

            instance.Running.OnValueChanged += HandleInstanceRunning;
        }


        private void HandleInstanceRunning(bool outgoing,
                                           bool incoming)
        {
            if (incoming)
            {
                Connect();
            }
            else
            {
                Disconnect();
            }
        }


        private void OnDisable()
        {
            if (!instance) return;

            instance.Running.OnValueChanged -= HandleInstanceRunning;
        }


        [ContextMenu("Connect")]
        void Connect()
        {
            tracer = instance._tracer;

            tracer.OnCallStackChange += HandleCallStackChange;
        }


        private void HandleCallStackChange()
        {
            text.text = string.Empty;
            
            foreach (var (depth, methodName) in tracer.CallStack)
            {
                for (var indent = depth;
                     indent > 0;
                     indent--) text.text = "    " + text.text;

                text.text = $"• {methodName}\n" + text.text;
            }
        }
        
        [ContextMenu("Disconnect")]
        void Disconnect()
        {
            tracer.OnCallStackChange -= HandleCallStackChange;

            tracer = null;
        }
        
        
    }
}

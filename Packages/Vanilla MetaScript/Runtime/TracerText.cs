using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vanilla.MetaScript
{
    public class TracerText : MonoBehaviour
    {

        [SerializeField]
        public MetaTaskInstance instance;
        
        [NonSerialized] private Tracer tracer;
        
        public Text text;


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

            tracer.OnDepthChange += HandleDepthChange;
        }


        private void HandleDepthChange()
        {
            // You need to know the depth per-stack-call in order to properly indent 🤦🏻
            // Unless you only add and remove 1 line at a time 😂

            text.text = "";
            
            var depth = -1;
            
            foreach (var s in tracer.CallStack)
            {
                depth++;

                for (var indent = tracer.CallStack.Count;
                     indent > 0;
                     indent--)
                {
//                    text.text += "    ";
                    text.text =  "    " + text.text;
                }

//                text.text += $"• {s}\n";
                text.text = $"• {s}\n" + text.text;
            }
        }
        
        [ContextMenu("Disconnect")]
        void Disconnect()
        {
            tracer.OnDepthChange -= HandleDepthChange;

            tracer = null;
        }
        
        
    }
}

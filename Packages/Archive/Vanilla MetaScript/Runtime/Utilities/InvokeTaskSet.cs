using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.LowLevel;

namespace Vanilla.MetaScript
{

    public class InvokeTaskSet : MonoBehaviour
    {

        public TaskSet target;

        public enum InvokeTiming
        {

            Awake,
            OnEnable,
            Start

        }

        public InvokeTiming invokeTiming = InvokeTiming.Start;

        public void Awake()
        {
            if (invokeTiming == InvokeTiming.Awake)
            {
                target.Run();
            }
        }

        public void Start()
        {
            if (invokeTiming == InvokeTiming.Start)
            {
                target.Run();
            }
        }
        
        public void OnEnable()
        {
            if (invokeTiming == InvokeTiming.OnEnable)
            {
                target.Run();
            }
        }

    }

}
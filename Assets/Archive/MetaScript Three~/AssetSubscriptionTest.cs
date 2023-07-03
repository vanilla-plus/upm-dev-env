using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.DataAssets;

namespace Vanilla.MetaScript.Three
{
    public class AssetSubscriptionTest : MonoBehaviour
    {

        public float myOldValue;
        public float myNewValue;

        public FloatSocket floatSocket;

        public UniTask Initialize()
        {
            floatSocket.OnChanged += UpdateMyLocalValue;

            return default;
        }

        public void UpdateMyLocalValue(float outgoing,
                                       float incoming)
        {
            myOldValue = outgoing;
            myNewValue = incoming;
        }

    }
}
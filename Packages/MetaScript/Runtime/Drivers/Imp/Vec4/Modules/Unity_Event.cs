using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.MetaScript.Drivers.Vec4
{
    [Serializable]
    public class Unity_Event : Module
    {

        public UnityEvent<Vector4> OnSet = new();

        public override void OnValidate(Driver<Vector4> driver) => OnSet.Invoke(driver.Asset.Source.Value);

        public override void Init(Driver<Vector4> driver) => TryConnectSet(driver);
        public override void DeInit(Driver<Vector4> driver) => TryDisconnectSet(driver);
        
        protected override void HandleSet(Vector4 value) => OnSet.Invoke(value);

    }
}

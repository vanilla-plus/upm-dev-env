using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Vec4
{
    [Serializable]
    public class Unity_Event : Module
    {

        [SerializeField]
        public UnityEvent<Vector4> onValueChange = new();
        public UnityEvent<Vector4> OnValueChange => onValueChange;

        public override void OnValidate(Driver<Vector4> driver) => OnValueChange.Invoke(driver.Asset.Delta.Value);

        public override void Init(Driver<Vector4> driver) => HandleValueChange(driver.Asset.Delta.Value);

        public override void HandleValueChange(Vector4 value) => OnValueChange.Invoke(value);

    }
}

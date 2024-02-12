using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Vec3
{
    [Serializable]
    public class Unity_Event : Module
    {

        [SerializeField]
        public UnityEvent<Vector3> onValueChange = new();
        public UnityEvent<Vector3> OnValueChange => onValueChange;

        public override void OnValidate(Driver<Vector3> driver) => OnValueChange.Invoke(driver.Asset.Delta.Value);

        public override void Init(Driver<Vector3> driver) => HandleValueChange(driver.Asset.Delta.Value);

        public override void HandleValueChange(Vector3 value) => OnValueChange.Invoke(value);

    }
}

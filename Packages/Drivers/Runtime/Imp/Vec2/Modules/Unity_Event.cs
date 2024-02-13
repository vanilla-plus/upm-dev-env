using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Vec2
{
    [Serializable]
    public class Unity_Event : Module
    {

        [SerializeField]
        public UnityEvent<Vector2> onValueChange = new();
        public UnityEvent<Vector2> OnValueChange => onValueChange;

        public override void OnValidate(Driver<Vector2> driver) => OnValueChange.Invoke(driver.Asset.Source.Value);

        public override void Init(Driver<Vector2> driver) => HandleValueChange(driver.Asset.Source.Value);

        public override void HandleValueChange(Vector2 value) => OnValueChange.Invoke(value);

    }
}

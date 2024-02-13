using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Float
{
    [Serializable]
    public class Unity_Event : Module
    {

        [SerializeField]
        public UnityEvent<float> onValueChange = new();
        public UnityEvent<float> OnValueChange => onValueChange;

        public override void OnValidate(Driver<float> driver) => OnValueChange.Invoke(driver.Asset.Source.Value);

        public override void Init(Driver<float> driver) => HandleValueChange(driver.Asset.Source.Value);

        public override void HandleValueChange(float value) => OnValueChange.Invoke(value);

    }
}

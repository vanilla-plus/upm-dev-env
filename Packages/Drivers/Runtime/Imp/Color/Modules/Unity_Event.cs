using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.Drivers.Color
{
    [Serializable]
    public class Unity_Event : Module
    {

        [SerializeField]
        public UnityEvent<UnityEngine.Color> onValueChange = new();
        public UnityEvent<UnityEngine.Color> OnValueChange => onValueChange;

        public override void OnValidate(Driver<UnityEngine.Color> driver) => OnValueChange.Invoke(driver.Asset.Source.Value);

        public override void Init(Driver<UnityEngine.Color> driver) => HandleValueChange(driver.Asset.Source.Value);

        public override void HandleValueChange(UnityEngine.Color value) => OnValueChange.Invoke(value);

    }
}

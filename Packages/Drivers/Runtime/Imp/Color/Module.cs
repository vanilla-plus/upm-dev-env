using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Drivers.Color
{
    
    [Serializable]
    public abstract class Module : Module<UnityEngine.Color>
    {

        public override void HandleValueChange(UnityEngine.Color value) => Debug.Log(value);

    }
}

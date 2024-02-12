using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Drivers.Float
{
    
    [Serializable]
    public abstract class Module : Module<float>
    {

        public override void HandleValueChange(float value) => Debug.Log(value);

    }
}

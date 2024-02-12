using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Drivers.Vec4
{
    
    [Serializable]
    public abstract class Module : Module<Vector4>
    {

        public override void HandleValueChange(Vector4 value) => Debug.Log(value);

    }
}

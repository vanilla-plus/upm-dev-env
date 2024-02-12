using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Drivers.Vec3
{
    
    [Serializable]
    public abstract class Module : Module<Vector3>
    {

        public override void HandleValueChange(Vector3 value) => Debug.Log(value);

    }
}

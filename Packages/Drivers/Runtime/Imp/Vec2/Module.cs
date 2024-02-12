using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Drivers.Vec2
{
    
    [Serializable]
    public abstract class Module : Module<Vector2>
    {

        public override void HandleValueChange(Vector2 value) => Debug.Log(value);

    }
}

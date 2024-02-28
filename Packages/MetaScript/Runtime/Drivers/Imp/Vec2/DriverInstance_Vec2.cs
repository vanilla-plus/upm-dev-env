using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MetaScript.Drivers.Vec2
{
    
    [Serializable]
    public class DriverInstance_Vec2 : DriverInstance<Vector2>
    {

        [SerializeField]
        private Driver[] _drivers = Array.Empty<Driver>();
        public override Driver<Vector2>[] Drivers => _drivers;

    }
}

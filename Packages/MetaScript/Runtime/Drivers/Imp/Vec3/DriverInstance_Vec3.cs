using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MetaScript.Drivers.Vec3
{
    
    [Serializable]
    public class DriverInstance_Vec3 : DriverInstance<Vector3>
    {

        [SerializeField]
        private Driver[] _drivers = Array.Empty<Driver>();
        public override Driver<Vector3>[] Drivers => _drivers;

    }
}

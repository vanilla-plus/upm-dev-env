using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MetaScript.Drivers.Vec4
{
    
    [Serializable]
    public class DriverInstance_Vec4 : DriverInstance<Vector4>
    {

        [SerializeField]
        private Driver[] _drivers = Array.Empty<Driver>();
        public override Driver<Vector4>[] Drivers => _drivers;

    }
}

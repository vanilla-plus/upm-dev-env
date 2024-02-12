using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Drivers.Float
{
    
    [Serializable]
    public class DriverInstance_Float : DriverInstance<float>
    {

        [SerializeField]
        private Driver[] _drivers = Array.Empty<Driver>();
        public override Driver<float>[] Drivers => _drivers;

    }
}

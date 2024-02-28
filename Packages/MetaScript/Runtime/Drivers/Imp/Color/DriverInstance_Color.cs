using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MetaScript.Drivers.Color
{
    
    [Serializable]
    public class DriverInstance_Color : DriverInstance<UnityEngine.Color>
    {

        [SerializeField]
        private Driver[] _drivers = Array.Empty<Driver>();
        public override Driver<UnityEngine.Color>[] Drivers => _drivers;

    }
}

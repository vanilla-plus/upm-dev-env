using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers.Float
{
    
    [Serializable]
    public class DriverInstance_Float : DriverInstance<float, FloatSource, FloatAsset, Module, Driver>
    {

        [SerializeField]
        private Driver[] _drivers = Array.Empty<Driver>();
        public override Driver[] Drivers => _drivers;

    }
}

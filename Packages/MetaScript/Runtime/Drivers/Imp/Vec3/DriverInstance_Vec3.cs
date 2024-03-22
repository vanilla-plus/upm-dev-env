using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers.Vec3
{
    
    [Serializable]
    public class DriverInstance_Vec3 : DriverInstance<Vector3, Vec3Source, Vec3Asset, Module, Driver>
    {

        [SerializeField]
        private Driver[] _drivers = Array.Empty<Driver>();
        public override Driver[] Drivers => _drivers;

    }
}

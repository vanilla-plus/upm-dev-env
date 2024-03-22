using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers.Vec4
{
    
    [Serializable]
    public class DriverInstance_Vec4 : DriverInstance<Vector4, Vec4Source, Vec4Asset, Module, Driver>
    {

        [SerializeField]
        private Driver[] _drivers = Array.Empty<Driver>();
        public override Driver[] Drivers => _drivers;

    }
}

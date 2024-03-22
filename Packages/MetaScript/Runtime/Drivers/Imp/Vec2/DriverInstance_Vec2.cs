using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers.Vec2
{
    
    [Serializable]
    public class DriverInstance_Vec2 : DriverInstance<Vector2, Vec2Source, Vec2Asset, Module, Driver>
    {

        [SerializeField]
        private Driver[] _drivers = Array.Empty<Driver>();
        public override Driver[] Drivers => _drivers;

    }
}

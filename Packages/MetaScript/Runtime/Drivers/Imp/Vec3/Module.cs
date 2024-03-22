using System;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers.Vec3
{
    
    [Serializable]
    public abstract class Module : Module<Vector3, Vec3Source, Vec3Asset, Module, Driver> { }
    
}

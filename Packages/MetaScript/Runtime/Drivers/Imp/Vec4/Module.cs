using System;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers.Vec4
{
    
    [Serializable]
    public abstract class Module : Module<Vector4, Vec4Source, Vec4Asset, Module, Driver> { }
    
}

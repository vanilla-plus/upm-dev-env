using System;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers.Vec2
{
    
    [Serializable]
    public abstract class Module : Module<Vector2, Vec2Source, Vec2Asset, Module, Driver> { }
    
}

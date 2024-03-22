using System;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers.Color
{
    
    [Serializable]
    public abstract class Module : Module<UnityEngine.Color, ColorSource, ColorAsset, Module, Driver> { }
    
}

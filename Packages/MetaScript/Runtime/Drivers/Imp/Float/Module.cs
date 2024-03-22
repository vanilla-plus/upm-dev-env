using System;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers.Float
{
    
    [Serializable]
    public abstract class Module : Module<float, FloatSource, FloatAsset, Module, Driver> { }
}

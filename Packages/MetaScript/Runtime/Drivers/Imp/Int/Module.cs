using System;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers.Int
{
    
	[Serializable]
	public abstract class Module : Module<int, IntSource, IntAsset, Module, Driver> { }
	

}
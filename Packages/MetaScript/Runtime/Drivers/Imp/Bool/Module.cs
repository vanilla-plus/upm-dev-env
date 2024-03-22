using System;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers.Bool
{
    
	[Serializable]
	public abstract class Module : Module<bool, BoolSource, BoolAsset, Module, Driver> { }
	
}
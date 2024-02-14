using System;

using Vanilla.DataSources;

namespace Vanilla.DataAssets
{
    
	[Serializable]
	public class Set_Bool_Source : Set_Source<bool, BoolSource, AssetBoolSource> { }

}
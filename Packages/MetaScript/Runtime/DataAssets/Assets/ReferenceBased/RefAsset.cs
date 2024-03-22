
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
	public abstract class RefAsset<T,S> : DataAsset<T,S>
		where T : class
		where S : class, IRefSource<T,S>
	{

	}

}
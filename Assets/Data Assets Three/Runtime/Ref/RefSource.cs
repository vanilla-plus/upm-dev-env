using System;

namespace Vanilla.DataAssets.Three
{

	[Serializable]
	public abstract class RefSource<TType> : GenericSource<TType>
		where TType : class { }

}
using System;

namespace Vanilla.DataAssets.Three
{

	[Serializable]
	public abstract class RefBinding<TType, TSource> : GenericBinding<TType, TSource>
		where TType : class
		where TSource : RefSource<TType> { }

}
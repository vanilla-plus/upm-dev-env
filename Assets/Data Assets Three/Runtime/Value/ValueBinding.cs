using System;

namespace Vanilla.DataAssets.Three
{

	[Serializable]
	public abstract class ValueBinding<TType, TSource> : GenericBinding<TType, TSource>
		where TType : unmanaged
		where TSource : ValueSource<TType> { }

}
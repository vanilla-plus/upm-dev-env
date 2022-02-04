using System;

namespace Vanilla.DataAssets.Three
{

	[Serializable]
	public abstract class StructBinding<TType, TSource> : GenericBinding<TType, TSource>
		where TType : struct
		where TSource : StructSource<TType> { }

}
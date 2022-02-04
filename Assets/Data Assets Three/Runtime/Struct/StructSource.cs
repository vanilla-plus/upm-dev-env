using System;

namespace Vanilla.DataAssets.Three
{

	[Serializable]
	public abstract class StructSource<TType> : GenericSource<TType>
		where TType : struct { }

}
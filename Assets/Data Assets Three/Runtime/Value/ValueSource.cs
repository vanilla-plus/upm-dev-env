using System;

namespace Vanilla.DataAssets.Three
{

	[Serializable]
	public abstract class ValueSource<TType> : GenericSource<TType>
		where TType : unmanaged { }

}
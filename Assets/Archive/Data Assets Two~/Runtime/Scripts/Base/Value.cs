using System;

namespace Vanilla.DataAssets // ----------------------------------------------------------------------------------------------------------- Value //
{

	[Serializable]
	public abstract class ValueSocket<TType, TSocket, TAsset, TAccessor> : GenericSocket<TType, TSocket, TAsset, TAccessor> // ----------- Socket //
		where TType : unmanaged
		where TSocket : ValueSocket<TType, TSocket, TAsset, TAccessor>
		where TAsset : ValueAsset<TType, TSocket, TAsset, TAccessor>
		where TAccessor : ValueAccessor<TType, TSocket, TAsset, TAccessor> { }

	[Serializable]
	public abstract class ValueAsset<TType, TSocket, TAsset, TAccessor> : GenericAsset<TType, TSocket, TAsset, TAccessor> // -------------- Asset //
		where TType : unmanaged
		where TSocket : ValueSocket<TType, TSocket, TAsset, TAccessor>
		where TAsset : ValueAsset<TType, TSocket, TAsset, TAccessor>
		where TAccessor : ValueAccessor<TType, TSocket, TAsset, TAccessor> { }

	[Serializable]
	public abstract class ValueAccessor<TType, TSocket, TAsset, TAccessor> : GenericAccessor<TType, TSocket, TAsset, TAccessor> // ---- Processors //
		where TType : unmanaged
		where TAsset : ValueAsset<TType, TSocket, TAsset, TAccessor>
		where TSocket : ValueSocket<TType, TSocket, TAsset, TAccessor>
		where TAccessor : ValueAccessor<TType, TSocket, TAsset, TAccessor> { }

}
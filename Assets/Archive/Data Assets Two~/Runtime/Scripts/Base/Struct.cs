using System;

namespace Vanilla.DataAssets // ---------------------------------------------------------------------------------------------------------- Struct //
{

	[Serializable]
	public abstract class StructSocket<TType, TSocket, TAsset, TAccessor> : GenericSocket<TType, TSocket, TAsset, TAccessor> // ---------- Socket //
		where TType : struct
		where TSocket : StructSocket<TType, TSocket, TAsset, TAccessor>
		where TAsset : StructAsset<TType, TSocket, TAsset, TAccessor>
		where TAccessor : StructAccessor<TType, TSocket, TAsset, TAccessor> { }

	[Serializable]
	public abstract class StructAsset<TType, TSocket, TAsset, TAccessor> : GenericAsset<TType, TSocket, TAsset, TAccessor> // ------------- Asset //
		where TType : struct
		where TSocket : StructSocket<TType, TSocket, TAsset, TAccessor>
		where TAsset : StructAsset<TType, TSocket, TAsset, TAccessor>
		where TAccessor : StructAccessor<TType, TSocket, TAsset, TAccessor> { }

	[Serializable]
	public abstract class StructAccessor<TType, TSocket, TAsset, TAccessor> : GenericAccessor<TType, TSocket, TAsset, TAccessor> // --- Processors //
		where TType : struct
		where TAsset : StructAsset<TType, TSocket, TAsset, TAccessor>
		where TSocket : StructSocket<TType, TSocket, TAsset, TAccessor>
		where TAccessor : StructAccessor<TType, TSocket, TAsset, TAccessor> { }

}
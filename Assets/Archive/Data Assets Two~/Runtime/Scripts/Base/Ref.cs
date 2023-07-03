using System;

namespace Vanilla.DataAssets // ------------------------------------------------------------------------------------------------------- Reference //
{

    [Serializable]
    public class RefSocket<TType, TSocket, TAsset, TAccessor> : GenericSocket<TType, TSocket, TAsset, TAccessor> // ---------------------- Socket //
        where TType : class
        where TSocket : RefSocket<TType, TSocket, TAsset, TAccessor>
        where TAsset : RefAsset<TType, TSocket, TAsset, TAccessor>
        where TAccessor : RefAccessor<TType, TSocket, TAsset, TAccessor> { }

    [Serializable]
    public abstract class RefAsset<TType, TSocket, TAsset, TAccessor> : GenericAsset<TType, TSocket, TAsset, TAccessor> // ---------------- Asset //
        where TType : class
        where TSocket : RefSocket<TType, TSocket, TAsset, TAccessor>
        where TAsset : RefAsset<TType, TSocket, TAsset, TAccessor>
        where TAccessor : RefAccessor<TType, TSocket, TAsset, TAccessor> { }

    [Serializable]
    public abstract class RefAccessor<TType, TSocket, TAsset, TAccessor> : GenericAccessor<TType, TSocket, TAsset, TAccessor> // --------- Processors //
        where TType : class
        where TSocket : RefSocket<TType, TSocket, TAsset, TAccessor>
        where TAsset : RefAsset<TType, TSocket, TAsset, TAccessor>
        where TAccessor : RefAccessor<TType, TSocket, TAsset, TAccessor> { }

}
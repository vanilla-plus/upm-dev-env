using System;

using static UnityEngine.Debug;

namespace Vanilla.DataAssets
{

    // ------------------------------------------------------------------------------------------------------------------------------- Interfaces //

    // ------------------------------------------------------------------------------------------------------------------------------------ Asset //

    public interface IDataAsset<TType, TAsset, TSocket, TGetter, TSetter>
    {

        string Description
        {
            get;
        }

        TType Value
        {
            get;
            set;
        }

        TGetter[] Getters
        {
            get;
            set;
        }

        TSetter[] Setters
        {
            get;
            set;
        }

        Action<TType> OnBroadcast
        {
            get;
            set;
        }

        Action<TType, TType> OnSet
        {
            get;
            set;
        }

        void OnValidate();

        void Awake();

        void OnEnable();

        void OnDisable();

        void Broadcast();

    }

    // ----------------------------------------------------------------------------------------------------------------------------------- Socket //

    public interface ISocket<TType, TAsset, TSocket, TGetter, TSetter>
        where TAsset : IDataAsset<TType, TAsset, TSocket, TGetter, TSetter>
        where TSocket : ISocket<TType, TAsset, TSocket, TGetter, TSetter>
        where TGetter : IGetter<TType, TAsset, TSocket, TGetter, TSetter>
        where TSetter : ISetter<TType, TAsset, TSocket, TGetter, TSetter>
    {

        TAsset Asset
        {
            get;
            set;
        }
        
        TType Fallback
        {
            get;
            set;
        }

        bool UseFallback
        {
            get;
            set;
        }
        
        Action<TType> OnBroadcast
        {
            get;
            set;
        }

        Action<TType, TType> OnSet
        {
            get;
            set;
        }

        void PlugInToAsset();

        void UnplugFromAsset();

        void PlugActionIntoSocket(Action<TType> broadcastAction, Action<TType,TType> setAction);

        void UnplugActionFromSocket(Action<TType> broadcastAction, Action<TType,TType> setAction);

        TType Get();

        void Set(TType newValue);

    }



    // ----------------------------------------------------------------------------------------------------------------------------------- Getter //

    public interface IGetter<TType, TAsset, TSocket, TGetter, TSetter>
        where TAsset : IDataAsset<TType, TAsset, TSocket, TGetter, TSetter>
        where TSocket : ISocket<TType, TAsset, TSocket, TGetter, TSetter>
        where TGetter : IGetter<TType, TAsset, TSocket, TGetter, TSetter>
        where TSetter : ISetter<TType, TAsset, TSocket, TGetter, TSetter>
    {

        void Get(TAsset asset);

    }

    // ----------------------------------------------------------------------------------------------------------------------------------- Setter //

    public interface ISetter<TType, TAsset, TSocket, TGetter, TSetter>
        where TAsset : IDataAsset<TType, TAsset, TSocket, TGetter, TSetter>
        where TSocket : ISocket<TType, TAsset, TSocket, TGetter, TSetter>
        where TGetter : IGetter<TType, TAsset, TSocket, TGetter, TSetter>
        where TSetter : ISetter<TType, TAsset, TSocket, TGetter, TSetter>
    {

        bool Set(TAsset asset,
                 ref TType outgoing,
                 ref TType incoming);

    }

}
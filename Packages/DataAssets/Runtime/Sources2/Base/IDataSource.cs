using System;

using UnityEngine;

namespace Vanilla.DataSources
{
    public interface IDataSource<T> : ISerializationCallbackReceiver
    {
        string Name
        {
            get;
            set;
        }

        T Value
        {
            get;
            set;
        }

        Action<T> OnSet
        {
            get;
            set;
        }

        Action<T,T> OnSetWithHistory
        {
            get;
            set;
        }

    }
}

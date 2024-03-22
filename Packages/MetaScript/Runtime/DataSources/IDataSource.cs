using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
    public interface IDataSource<T> : ISerializationCallbackReceiver
    {

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

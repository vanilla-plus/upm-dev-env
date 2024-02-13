using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.DataSources
{
    public interface IRangedDataSource<T> : IStructSource<T>
    {

        T Min
        {
            get;
            set;
        }
        
        T Max
        {
            get;
            set;
        }

        ProtectedBoolSource AtMin
        {
            get;
            set;
        }

        ProtectedBoolSource AtMax
        {
            get;
            set;
        }

    }
}

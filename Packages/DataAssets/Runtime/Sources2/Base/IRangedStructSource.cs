using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.DataSources
{
    public interface IRangedDataSource<T> : IStructSource<T> where T : struct
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
        }

        ProtectedBoolSource AtMax
        {
            get;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
    public interface IRangedDataSource<T> : IStructSource<T>
    {

//        IDataSource<T> Min
//        {
//            get;
//            set;
//        }
//        
//        IDataSource<T> Max
//        {
//            get;
//            set;
//        }
        
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

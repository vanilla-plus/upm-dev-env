using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
    public interface IRangedSource<T> : IStructSource<T>
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

        BoolSource_Protected AtMin
        {
            get;
            set;
        }

        BoolSource_Protected AtMax
        {
            get;
            set;
        }

    }
}

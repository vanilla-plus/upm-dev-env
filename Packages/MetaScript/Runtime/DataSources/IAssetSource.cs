using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;

namespace Vanilla.MetaScript.DataSources
{
    public interface IAssetSource<T,S,A>
        where S : IDataSource<T>
        where A : DataAsset<T,S>
    {
        
        A Asset
        {
            get;
            set;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.DataAssets;

namespace Vanilla.DataSources
{
    public interface IAssetSource<T> : IDataSource<T>
    {


        
        DataAsset<T> Asset
        {
            get;
            set;
        }
        
    }
}

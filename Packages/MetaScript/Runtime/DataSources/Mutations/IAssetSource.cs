using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;

namespace Vanilla.MetaScript.DataSources
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

using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
    
    [Serializable]
    public class BoolSource_Asset : BoolSource, IAssetSource<bool, BoolSource_Observable, BoolAsset>
    {

        [SerializeField]
        private BoolAsset _asset;
        public BoolAsset Asset
        {
            get => _asset;
            set => _asset = value;
        }
        
        public override bool Value
        {
            get => _asset.Source.Value;
            set => _asset.Source.Value = value;
        }
        
        public override string ToString() => Asset ?
                                                 Asset.Source ?
                                                     Asset.Source is BoolSource_Asset ?
                                                         Asset.Source.ToString() : // Drill into the nested source for a name
                                                         Asset.name : // This assumes that no new BoolSource children have unique ToStrings like AssetBoolSource does!
                                                     Utility.Unknown :
                                                 Utility.Unknown;

    }
}

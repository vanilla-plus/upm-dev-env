using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
    
    [Serializable]
    public class AssetBoolSource : BoolSource, IAssetSource<bool, BoolSource, BoolAsset>
    {

//        [SerializeField]
//        private string _name = "Unnamed BoolAsset";
//        public string Name
//        {
//            get => _name;
//            set => _name = value;
//        }

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
        

        public override void OnBeforeSerialize() { }

        public override void OnAfterDeserialize() { }
        
    }
}

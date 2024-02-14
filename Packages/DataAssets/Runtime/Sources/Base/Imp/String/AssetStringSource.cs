using System;

using UnityEngine;

using Vanilla.DataSources;

namespace Vanilla.DataAssets
{
    
	[Serializable]
	public class AssetStringSource : StringSource, IAssetSource<string>
	{

//        [SerializeField]
//        private string _name = "Unnamed StringAsset";
//        public string Name
//        {
//            get => _name;
//            set => _name = value;
//        }

		[SerializeField]
		private StringAsset _asset;
		public DataAsset<string> Asset
		{
			get => _asset;
			set => _asset = value as StringAsset;
		}
        
		public override string Value
		{
			get => _asset.Source.Value;
			set => _asset.Source.Value = value;
		}
        

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }
        
	}
}
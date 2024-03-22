using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	public class AssetStringSource : StringSource, IAssetSource<string, StringSource, StringAsset>
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
		public StringAsset Asset
		{
			get => _asset;
			set => _asset = value;
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
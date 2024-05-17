using System;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;

namespace Vanilla.MetaScript.DataSources.Strings
{
    
	[Serializable]
	public class StringSource_Asset : StringSource, IAssetSource<string, StringSource, StringAsset>
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


//		public override string ToString() => Asset != null ?
//			                                     Asset.Source != null ?
//				                                     Asset.Source.Value :
//				                                      Utility.Unknown :
//			                                     Utility.Unknown;

	}
}
using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	public class AssetColorSource : ColorSource, IAssetSource<Color, ColorSource, ColorAsset>
	{

//		[SerializeField]
//		private string _name = "Unnamed ColorAsset";
//		public string Name
//		{
//			get => _name;
//			set => _name = value;
//		}
		
		[SerializeField]
		private ColorAsset _asset;
		public ColorAsset Asset
		{
			get => _asset;
			set => _asset = value as ColorAsset;
		}
        
		public override Color Value
		{
			get => _asset.Source.Value;
			set => _asset.Source.Value = value;
		}
        

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }
        
	}
}
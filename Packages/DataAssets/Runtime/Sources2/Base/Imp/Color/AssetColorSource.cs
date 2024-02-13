using System;

using UnityEngine;

using Vanilla.DataSources;

namespace Vanilla.DataAssets
{
    
	[Serializable]
	public class AssetColorSource : ColorSource, IAssetSource<Color>
	{

		[SerializeField]
		private ColorAsset _asset;
		public DataAsset<Color> Asset
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
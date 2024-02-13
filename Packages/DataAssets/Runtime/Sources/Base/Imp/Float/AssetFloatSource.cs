using System;

using UnityEngine;

using Vanilla.DataSources;

namespace Vanilla.DataAssets
{
    
	[Serializable]
	public class AssetFloatSource : FloatSource, IAssetSource<float>
	{
		

		
		[SerializeField]
		private FloatAsset _asset;
		public DataAsset<float> Asset
		{
			get => _asset;
			set => _asset = value as FloatAsset;
		}
        
		public override float Value
		{
			get => _asset.Source.Value;
			set => _asset.Source.Value = value;
		}
        

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }
        
	}
}
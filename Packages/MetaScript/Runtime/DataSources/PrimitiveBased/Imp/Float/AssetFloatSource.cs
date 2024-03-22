using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	public class AssetFloatSource : FloatSource, IAssetSource<float, FloatSource, FloatAsset>
	{
		

		
		[SerializeField]
		private FloatAsset _asset;
		public FloatAsset Asset
		{
			get => _asset;
			set => _asset = value;
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
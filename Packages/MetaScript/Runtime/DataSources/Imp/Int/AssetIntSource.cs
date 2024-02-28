using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	public class AssetIntSource : IntSource, IAssetSource<int>
	{

		[SerializeField]
		private IntAsset _asset;
		public DataAsset<int> Asset
		{
			get => _asset;
			set => _asset = value as IntAsset;
		}
        
		public override int Value
		{
			get => _asset.Source.Value;
			set => _asset.Source.Value = value;
		}
        

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }
        
	}
}
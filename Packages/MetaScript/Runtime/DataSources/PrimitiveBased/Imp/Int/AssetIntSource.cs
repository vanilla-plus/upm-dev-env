using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.MetaScript.Flow;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	public class AssetIntSource : IntSource, IAssetSource<int, IntSource, IntAsset>
	{

		[SerializeField]
		private IntAsset _asset;
		public IntAsset Asset
		{
			get => _asset;
			set => _asset = value;
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
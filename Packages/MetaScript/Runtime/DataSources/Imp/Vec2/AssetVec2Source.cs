using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	public class AssetVec2Source : Vec2Source, IAssetSource<Vector2>
	{
		

		
		[SerializeField]
		private Vec2Asset _asset;
		public DataAsset<Vector2> Asset
		{
			get => _asset;
			set => _asset = value as Vec2Asset;
		}
        
		public override Vector2 Value
		{
			get => _asset.Source.Value;
			set => _asset.Source.Value = value;
		}
        

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }
        
	}
}
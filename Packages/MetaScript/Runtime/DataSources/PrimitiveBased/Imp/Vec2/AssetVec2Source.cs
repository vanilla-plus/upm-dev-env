using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	public class AssetVec2Source : Vec2Source, IAssetSource<Vector2, Vec2Source, Vec2Asset>
	{
		

		
		[SerializeField]
		private Vec2Asset _asset;
		public Vec2Asset Asset
		{
			get => _asset;
			set => _asset = value;
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
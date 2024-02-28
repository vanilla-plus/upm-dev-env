using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	public class AssetVec4Source : Vec4Source, IAssetSource<Vector4>
	{
		

		
		[SerializeField]
		private Vec4Asset _asset;
		public DataAsset<Vector4> Asset
		{
			get => _asset;
			set => _asset = value as Vec4Asset;
		}
        
		public override Vector4 Value
		{
			get => _asset.Source.Value;
			set => _asset.Source.Value = value;
		}
        

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }
        
	}
}
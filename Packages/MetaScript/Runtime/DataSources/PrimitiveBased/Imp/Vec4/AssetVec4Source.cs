using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	public class AssetVec4Source : Vec4Source, IAssetSource<Vector4, Vec4Source, Vec4Asset>
	{
		

		
		[SerializeField]
		private Vec4Asset _asset;
		public Vec4Asset Asset
		{
			get => _asset;
			set => _asset = value;
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
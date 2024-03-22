using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	public class AssetVec3Source : Vec3Source, IAssetSource<Vector3, Vec3Source, Vec3Asset>
	{
		

		
		[SerializeField]
		private Vec3Asset _asset;
		public Vec3Asset Asset
		{
			get => _asset;
			set => _asset = value;
		}
        
		public override Vector3 Value
		{
			get => _asset.Source.Value;
			set => _asset.Source.Value = value;
		}
        

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }
        
	}
}
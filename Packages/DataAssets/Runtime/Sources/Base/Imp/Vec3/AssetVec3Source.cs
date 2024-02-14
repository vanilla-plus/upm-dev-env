using System;

using UnityEngine;

using Vanilla.DataSources;

namespace Vanilla.DataAssets
{
    
	[Serializable]
	public class AssetVec3Source : Vec3Source, IAssetSource<Vector3>
	{
		

		
		[SerializeField]
		private Vec3Asset _asset;
		public DataAsset<Vector3> Asset
		{
			get => _asset;
			set => _asset = value as Vec3Asset;
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
using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	public class FloatSource_Asset : FloatSource, IAssetSource<float, FloatSource, FloatAsset>, IGettableSource<float>, ISettableSource<float>
	{
		
		[SerializeField]
		internal FloatAsset _asset;
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
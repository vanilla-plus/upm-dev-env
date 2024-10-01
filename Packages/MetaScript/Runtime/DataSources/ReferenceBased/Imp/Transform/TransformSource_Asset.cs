using System;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources.GenericComponent;

namespace Vanilla.MetaScript.DataSources
{

	[Serializable]
//	public class AssetTransformSource : ITransformSource, IAssetSource<Transform, AssetTransformSource, TransformAsset>
	public class TransformSource_Asset : IAssetComponentSource<Transform, ITransformSource, TransformAsset>, ITransformSource
	{

//		[SerializeField]
//		private string _name = "Unnamed AssetTransformSource";
//		public string Name
//		{
//			get => _name;
//			set => _name = value;
//		}
		
		[SerializeField]
		private TransformAsset _asset;
		public TransformAsset Asset
		{
			get => _asset;
			set => _asset = value;
		}

		public Transform Value
		{
			get => _asset.Source.Value;
			set => _asset.Source.Value = value;
		}

		public Action<Transform> OnSet
		{
			get => _asset.Source.OnSet;
			set => _asset.Source.OnSet = value;
		}

		public Action<Transform, Transform> OnSetWithHistory
		{
			get => _asset.Source.OnSetWithHistory;
			set => _asset.Source.OnSetWithHistory = value;
		}

		public void OnBeforeSerialize() { }

		public void OnAfterDeserialize() { }

	}

}
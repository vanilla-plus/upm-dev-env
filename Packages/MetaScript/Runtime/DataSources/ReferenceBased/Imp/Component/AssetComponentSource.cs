using System;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;

namespace Vanilla.MetaScript.DataSources.GenericComponent
{
    
//	[Serializable]
	public interface IAssetComponentSource<T,S,A> : IComponentSource<T,S>, IAssetSource<T,S,A> 
		where T : Component
		where S : class, IComponentSource<T,S>
		where A : ComponentAsset<T,S>
	{

//		[SerializeField]
//		private A _asset;
//		public A Asset
//		{
//			get => _asset;
//			set => _asset = value;
//		}
//        
//		public T Value
//		{
//			get => _asset.Source.Value;
//			set => _asset.Source.Value = value;
//		}
//
//		public Action<T> OnSet
//		{
//			get => _asset.Source.OnSet;
//			set => _asset.Source.OnSet = value;
//		}
//
//		public Action<T,T> OnSetWithHistory
//		{
//			get => _asset.Source.OnSetWithHistory;
//			set => _asset.Source.OnSetWithHistory = value;
//		}
//
//		public void OnBeforeSerialize() { }
//
//		public void OnAfterDeserialize() { }

	}
}
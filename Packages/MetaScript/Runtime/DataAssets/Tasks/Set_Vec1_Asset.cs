//using System;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//
//using Vanilla.MetaScript.DataAssets.Sources;
//using Vanilla.MetaScript;
//using Vanilla.TypeMenu;
//
//namespace Vanilla.MetaScript.DataAssets
//{
//
//	[Serializable]
//	public class Set_Float_Asset : MetaTask
//	{
//
//		[SerializeField]
//		public FloatAsset asset;
//
//		[SerializeReference]
//		[TypeMenu("yellow")]
//		public Float_Source FloatSource;
//
//		protected override bool CanAutoName() => asset != null && FloatSource != null;
//
//
//		protected override string CreateAutoName() => FloatSource switch
//		                                              {
//			                                              Direct_Float_Source directSource => $"Set [{asset.name}] to [{directSource.Value}]",
//			                                              Asset_Float_Source assetSource   => $"Set [{asset.name}] to the value of [{assetSource.asset.name}]",
//			                                              Delta_Float_Source deltaSource   => $"Set [{asset.name}] to the value of [{deltaSource.Delta.Name}]",
//			                                              _                               => "Unknown FloatSource Type"
//		                                              };
//
//
//		protected override UniTask<Scope> _Run(Scope scope)
//		{
//
//			if (asset == null)
//			{
//				Debug.LogError($"[{Name}] - null asset!");
//
//				return UniTask.FromResult(scope);
//			}
//
//			if (FloatSource == null)
//			{
//				Debug.LogError($"[{Name}] - null source!");
//
//				return UniTask.FromResult(scope);
//			}
//
//			asset.DeltaValue.Value = FloatSource.Get();
//
//			return UniTask.FromResult(scope);
//			
//		}
//
//	}
//
//}
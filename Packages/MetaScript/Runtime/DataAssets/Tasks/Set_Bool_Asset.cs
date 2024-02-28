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
//	public class Set_Bool_Asset : MetaTask
//	{
//
//		[SerializeField]
//		public BoolAsset asset;
//
//		[SerializeReference]
//		[TypeMenu("yellow")]
//		public Bool_Source BoolSource;
//
//		protected override bool CanAutoName() => asset != null && BoolSource != null;
//
//
//		protected override string CreateAutoName() => BoolSource switch
//		                                              {
//			                                              Direct_Bool_Source directSource => $"Set [{asset.name}] to [{directSource.Value}]",
//			                                              Asset_Bool_Source assetSource   => $"Set [{asset.name}] to the value of [{assetSource.asset.name}]",
//			                                              Delta_Bool_Source deltaSource   => $"Set [{asset.name}] to the value of [{deltaSource.Delta.Name}]",
//			                                              _                               => "Unknown BoolSource Type"
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
//			if (BoolSource == null)
//			{
//				Debug.LogError($"[{Name}] - null source!");
//
//				return UniTask.FromResult(scope);
//			}
//
//			asset.DeltaValue.Value = BoolSource.Get();
//
//			return UniTask.FromResult(scope);
//			
//		}
//
//	}
//
//}
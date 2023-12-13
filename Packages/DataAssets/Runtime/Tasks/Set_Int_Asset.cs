//using System;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//
//using Vanilla.DataAssets.Sources;
//using Vanilla.MetaScript;
//using Vanilla.TypeMenu;
//
//namespace Vanilla.DataAssets
//{
//
//	[Serializable]
//	public class Set_Int_Asset : MetaTask
//	{
//
//		[SerializeField]
//		public IntAsset asset;
//
//		[SerializeReference]
//		[TypeMenu("yellow")]
//		public Int_Source IntSource;
//
//		protected override bool CanAutoName() => asset != null && IntSource != null;
//
//
//		protected override string CreateAutoName() => IntSource switch
//		                                              {
//			                                              Direct_Int_Source directSource => $"Set [{asset.name}] to [{directSource.Value}]",
//			                                              Asset_Int_Source assetSource   => $"Set [{asset.name}] to the value of [{assetSource.asset.name}]",
//			                                              Delta_Int_Source deltaSource   => $"Set [{asset.name}] to the value of [{deltaSource.Delta.Name}]",
//			                                              _                               => "Unknown IntSource Type"
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
//			if (IntSource == null)
//			{
//				Debug.LogError($"[{Name}] - null source!");
//
//				return UniTask.FromResult(scope);
//			}
//
//			asset.DeltaValue.Value = IntSource.Get();
//
//			return UniTask.FromResult(scope);
//			
//		}
//
//	}
//
//}
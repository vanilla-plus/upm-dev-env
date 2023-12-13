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
//	public class Set_Vec1_Asset : MetaTask
//	{
//
//		[SerializeField]
//		public Vec1Asset asset;
//
//		[SerializeReference]
//		[TypeMenu("yellow")]
//		public Vec1_Source Vec1Source;
//
//		protected override bool CanAutoName() => asset != null && Vec1Source != null;
//
//
//		protected override string CreateAutoName() => Vec1Source switch
//		                                              {
//			                                              Direct_Vec1_Source directSource => $"Set [{asset.name}] to [{directSource.Value}]",
//			                                              Asset_Vec1_Source assetSource   => $"Set [{asset.name}] to the value of [{assetSource.asset.name}]",
//			                                              Delta_Vec1_Source deltaSource   => $"Set [{asset.name}] to the value of [{deltaSource.Delta.Name}]",
//			                                              _                               => "Unknown Vec1Source Type"
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
//			if (Vec1Source == null)
//			{
//				Debug.LogError($"[{Name}] - null source!");
//
//				return UniTask.FromResult(scope);
//			}
//
//			asset.DeltaValue.Value = Vec1Source.Get();
//
//			return UniTask.FromResult(scope);
//			
//		}
//
//	}
//
//}
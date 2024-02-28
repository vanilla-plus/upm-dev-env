//using System;
//using System.Collections;
//using System.Collections.Generic;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//
//using Vanilla.MetaScript.DataSources;
//using Vanilla.MetaScript;
//using Vanilla.TypeMenu;
//
//namespace Vanilla.MetaScript.DataAssets
//{
//
//	[Serializable]
//	public abstract class Interpolate_Source<PayloadType, Source, AssetSource> : MetaTask
//		where PayloadType : struct
//		where Source : IDataSource<PayloadType>
//		where AssetSource : IAssetSource<PayloadType>, new()
//	{
//
//		[SerializeReference]
//		[TypeMenu("red")]
//		public Source source;
//
//		[SerializeReference]
//		[TypeMenu("red")]
//		public Source target;
//
//		[SerializeReference]
//		[TypeMenu("magenta")]
//		public FloatSource speed = new DirectFloatSource()
//		                           {
//			                           Value = 1.0f
//		                           };
//
//		[SerializeReference]
//		[TypeMenu("magenta")]
//		public BoolSource useScaledTime = new DirectBoolSource
//		                                  {
//			                                  Value = true
//		                                  };
//		
//		protected override bool CanAutoName() => source != null && target != null;
//
//
//		protected override string CreateAutoName() => $"Set [{(source is AssetSource s && s.Asset != null ? s.Asset.name : source.GetType().Name)}] to [{(target is AssetSource t && t.Asset != null ? t.Asset.name : target.GetType().Name)}]";
//
//
//		protected override async UniTask<Scope> _Run(Scope scope)
//		{
//			if (scope.Cancelled) return scope;
//
//			if (source != null &&
//			    target != null)
//			{
//			// Because we start from the current value of set.Normal (which may have been previously Eased)
//			// There may be undesired jumps in the value of Normal when jumping back and forth between
//			// multiple iterations of this task in rapid succession?
//
//			var i    = source.Value;
//			var t    = target.Value;
//			var rate = speed.Value;
//
//			
//			if (i < t)
//			{
//				if (useScaledTime)
//				{
//					while (source.Value < StopAtValue)
//					{
//						if (scope.Cancelled) return scope;
//
//						i += Time.deltaTime * rate;
//
//						asset.DeltaValue.Value = easingCurve.Evaluate(i);
//
//						await UniTask.Yield();
//					}
//
//					asset.DeltaValue.Value = StopAtValue;
//				}
//				else
//				{
//					while (asset.DeltaValue.Value < StopAtValue)
//					{
//						if (scope.Cancelled) return scope;
//
//						i += Time.unscaledDeltaTime * rate;
//
//						asset.DeltaValue.Value = easingCurve.Evaluate(i);
//
//						await UniTask.Yield();
//					}
//
//					asset.DeltaValue.Value = StopAtValue;
//				}
//			}
//			else if (i > StopAtValue)
//			{
//				if (useScaledTime)
//				{
//					while (asset.DeltaValue.Value > StopAtValue)
//					{
//						if (scope.Cancelled) return scope;
//
//						i -= Time.deltaTime * rate;
//
//						asset.DeltaValue.Value = easingCurve.Evaluate(i);
//
//						await UniTask.Yield();
//					}
//
//					asset.DeltaValue.Value = StopAtValue;
//				}
//				else
//				{
//					while (asset.DeltaValue.Value > StopAtValue)
//					{
//						if (scope.Cancelled) return scope;
//
//						i -= Time.unscaledDeltaTime * rate;
//
//						asset.DeltaValue.Value = easingCurve.Evaluate(i);
//
//						await UniTask.Yield();
//					}
//
//					asset.DeltaValue.Value = StopAtValue;
//				}
//
//			}
//			}
//
//			return UniTask.FromResult(scope);
//		}
//
//	}
//
//}
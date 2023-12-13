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
//	public class Interpolate_Normal_Asset : MetaTask
//	{
//
//		[SerializeField]
//		public NormalAsset asset;
//
//		[SerializeReference]
//		[TypeMenu("yellow")]
//		public Vec1_Source target;
//
//		[SerializeField]
//		public bool UseScaledTime = true;
//
//		[SerializeField]
//		[Range(0.0f,
//		       1.0f)]
//		public float StopAtValue = 1.0f;
//
//		[Tooltip("Assuming the set is being driven from 0.0f, how many seconds should it take to reach its maximum of 1.0f?")]
//		[SerializeField]
//		public float secondsToTakeFromZero = 1.0f;
//
//		[SerializeField]
//		public AnimationCurve easingCurve = AnimationCurve.Linear(timeStart: 0.0f,
//		                                                          valueStart: 0.0f,
//		                                                          timeEnd: 1.0f,
//		                                                          valueEnd: 1.0f);
//
//		protected override bool CanAutoName() => asset != null && target != null;
//
//
//		protected override string CreateAutoName() => target switch
//		                                              {
//			                                              Direct_Vec1_Source directSource => $"Set [{asset.name}] to [{directSource.Value}]",
//			                                              Asset_Vec1_Source assetSource   => $"Set [{asset.name}] to the value of [{assetSource.asset.name}]",
//			                                              Delta_Vec1_Source deltaSource   => $"Set [{asset.name}] to the value of [{deltaSource.Delta.Name}]",
//			                                              _                               => "Unknown Vec1Source Type"
//		                                              };
//
//
//
//		protected override async UniTask<Scope> _Run(Scope scope)
//		{
//			if (asset == null)
//			{
//				Debug.LogError($"[{Name}] - null asset!");
//
//				return scope;
//			}
//
//			// Because we start from the current value of set.Normal (which may have been previously Eased)
//			// There may be undesired jumps in the value of Normal when jumping back and forth between
//			// multiple iterations of this task in rapid succession?
//
//			var i    = asset.Normal.Value;
//			var rate = 1.0f / secondsToTakeFromZero;
//
//			if (i < StopAtValue)
//			{
//				if (UseScaledTime)
//				{
//					while (asset.Normal.Value < StopAtValue)
//					{
//						if (scope.Cancelled) return scope;
//
//						i += Time.deltaTime * rate;
//
//						asset.Normal.Value = easingCurve.Evaluate(i);
//
//						await UniTask.Yield();
//					}
//
//					asset.Normal.Value = StopAtValue;
//				}
//				else
//				{
//					while (asset.Normal.Value < StopAtValue)
//					{
//						if (scope.Cancelled) return scope;
//
//						i += Time.unscaledDeltaTime * rate;
//
//						asset.Normal.Value = easingCurve.Evaluate(i);
//
//						await UniTask.Yield();
//					}
//
//					asset.Normal.Value = StopAtValue;
//				}
//			}
//			else if (i > StopAtValue)
//			{
//				if (UseScaledTime)
//				{
//					while (asset.Normal.Value > StopAtValue)
//					{
//						if (scope.Cancelled) return scope;
//
//						i -= Time.deltaTime * rate;
//
//						asset.Normal.Value = easingCurve.Evaluate(i);
//
//						await UniTask.Yield();
//					}
//
//					asset.Normal.Value = StopAtValue;
//				}
//				else
//				{
//					while (asset.Normal.Value > StopAtValue)
//					{
//						if (scope.Cancelled) return scope;
//
//						i -= Time.unscaledDeltaTime * rate;
//
//						asset.Normal.Value = easingCurve.Evaluate(i);
//
//						await UniTask.Yield();
//					}
//
//					asset.Normal.Value = StopAtValue;
//				}
//
//			}
//
//			return scope;
//		}
//
//	}
//
//}
//#if vanilla_metascript && cysharp_unitask
//
//using System;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//
//using Vanilla.MetaScript;
//
//namespace Vanilla.Drivers
//{
//
//	[Serializable]
//	public class Lerp_Driver_Global : MetaTask
//	{
//
//		[SerializeField]
//		public string DriverSetName;
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
//		protected override bool CanAutoName() => !string.IsNullOrEmpty(DriverSetName);
//
//
//		protected override string CreateAutoName() => $"Lerp [{DriverSetName}] drivers to [{StopAtValue}] over [{secondsToTakeFromZero}] seconds";
//
//
//		protected override async UniTask<Scope> _Run(Scope scope)
//		{
//			if (!NormalDriverInstance.GlobalDictionary.TryGetValue(key: DriverSetName,
//			                                                 value: out var set))
//			{
//				#if UNITY_EDITOR
//				Debug.LogError($"No DriverSet by the name [{DriverSetName}]");
//				#endif
//
//				return scope;
//			}
//
//			// Because we start from the current value of set.Normal (which may have been previously Eased)
//			// There may be undesired jumps in the value of Normal when jumping back and forth between
//			// multiple iterations of this task in rapid succession?
//
//			var i    = set.normal.Value;
//			var rate = 1.0f / secondsToTakeFromZero;
//
//			if (i < StopAtValue)
//			{
//				if (UseScaledTime)
//				{
//					while (set.normal.Value < StopAtValue)
//					{
//						if (scope.Cancelled) return scope;
//
//						i += Time.deltaTime * rate;
//
//						set.normal.Value = easingCurve.Evaluate(i);
//
//						await UniTask.Yield();
//					}
//
//					set.normal.Value = StopAtValue;
//				}
//				else
//				{
//					while (set.normal.Value < StopAtValue)
//					{
//						if (scope.Cancelled) return scope;
//
//						i += Time.unscaledDeltaTime * rate;
//
//						set.normal.Value = easingCurve.Evaluate(i);
//
//						await UniTask.Yield();
//					}
//
//					set.normal.Value = StopAtValue;
//				}
//			}
//			else if (i > StopAtValue)
//			{
//				if (UseScaledTime)
//				{
//					while (set.normal.Value > StopAtValue)
//					{
//						if (scope.Cancelled) return scope;
//
//						i -= Time.deltaTime * rate;
//
//						set.normal.Value = easingCurve.Evaluate(i);
//
//						await UniTask.Yield();
//					}
//
//					set.normal.Value = StopAtValue;
//				}
//				else
//				{
//					while (set.normal.Value > StopAtValue)
//					{
//						if (scope.Cancelled) return scope;
//
//						i -= Time.unscaledDeltaTime * rate;
//
//						set.normal.Value = easingCurve.Evaluate(i);
//
//						await UniTask.Yield();
//					}
//
//					set.normal.Value = StopAtValue;
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
//
//#endif
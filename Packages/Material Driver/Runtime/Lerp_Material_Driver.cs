#if vanilla_metascript

using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript;

namespace Vanilla.MaterialDriver
{

	[Serializable]
	public class Lerp_Material_Driver : MetaTask
	{

		[SerializeField]
		public string ProperySetName;
		
		[SerializeField]
		public bool UseScaledTime = true;
		
		[SerializeField]
		[Range(0.0f, 1.0f)]
		public float StopAtValue = 1.0f;

		[Tooltip("Assuming the set is being driven from 0.0f, how many seconds should it take to reach its maximum of 1.0f?")]
		[SerializeField]
		public float secondsToTakeFromZero = 1.0f;
		
		[SerializeField]
		public AnimationCurve easingCurve = AnimationCurve.Linear(timeStart: 0.0f,
		                                                          valueStart: 0.0f,
		                                                          timeEnd: 1.0f,
		                                                          valueEnd: 1.0f);

		protected override bool CanAutoName() => !string.IsNullOrEmpty(ProperySetName);


		protected override string CreateAutoName() => $"Lerp [{ProperySetName}] materials to [{StopAtValue}] over [{secondsToTakeFromZero}] seconds";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (!MaterialDriver.SetDictionary.TryGetValue(key: ProperySetName,
			                                              value: out var set))
			{
				#if UNITY_EDITOR
				Debug.LogError($"No PropertyDriverSet by the name [{ProperySetName}]");
				#endif

				return scope;
			}

			// Because we start from the current value of set.Normal (which may have been previously Eased)
			// There may be undesired jumps in the value of Normal when jumping back and forth between
			// multiple iterations of this task in rapid succession?

			var i    = set.Normal.Value;
			var rate = 1.0f / secondsToTakeFromZero;
			
			
			
			if (i < StopAtValue)
			{
				if (UseScaledTime)
				{
					while (set.Normal.Value < StopAtValue)
					{
						if (scope.Cancelled) return scope;
						
						i += Time.deltaTime * rate;

						set.Normal.Value = easingCurve.Evaluate(i);

						await UniTask.Yield();
					}

					set.Normal.Value = StopAtValue;
				}
				else
				{
					while (set.Normal.Value < StopAtValue)
					{
						if (scope.Cancelled) return scope;
						
						i += Time.unscaledDeltaTime * rate;

						set.Normal.Value = easingCurve.Evaluate(i);

						await UniTask.Yield();
					}

					set.Normal.Value = StopAtValue;
				}
			}
			else if (i > StopAtValue)
			{
				if (UseScaledTime)
				{
					while (set.Normal.Value > StopAtValue)
					{
						if (scope.Cancelled) return scope;
						
						i -= Time.deltaTime * rate;

						set.Normal.Value = easingCurve.Evaluate(i);

						await UniTask.Yield();
					}

					set.Normal.Value = StopAtValue;
				}
				else
				{
					while (set.Normal.Value > StopAtValue)
					{
						if (scope.Cancelled) return scope;
						
						i -= Time.unscaledDeltaTime * rate;

						set.Normal.Value = easingCurve.Evaluate(i);

						await UniTask.Yield();
					}

					set.Normal.Value = StopAtValue;
				}

			}

			return scope;
		}

	}

}

#endif
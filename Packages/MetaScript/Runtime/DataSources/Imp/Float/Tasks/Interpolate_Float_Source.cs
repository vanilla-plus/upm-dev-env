using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.MetaScript;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	public class Interpolate_Float_Source : MetaTask
	{

		[SerializeReference]
		[TypeMenu("red")]
		public FloatSource source;

		[SerializeReference]
		[TypeMenu("red")]
		public FloatSource target;

		[SerializeField] public float seconds = 1.0f;

		[SerializeReference]
		[TypeMenu("magenta")]
		public BoolSource useScaledTime = new DirectBoolSource
		                                  {
			                                  Value = true
		                                  };

		[SerializeField]
		public AnimationCurve easingCurve = AnimationCurve.Linear(timeStart: 0.0f,
		                                                          valueStart: 0.0f,
		                                                          timeEnd: 1.0f,
		                                                          valueEnd: 1.0f);
		
		protected override bool CanAutoName() => source != null && target != null;


		protected override string CreateAutoName() => $"Lerp [{(source is AssetFloatSource s && s.Asset != null ? s.Asset.name : source.GetType().Name)}] to [{(target is AssetFloatSource t && t.Asset != null ? t.Asset.name : target.GetType().Name)}] over [{seconds}] seconds";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return scope;

			if (source == null ||
			    target == null) return scope;

			// Because we start from the current value of set.Normal (which may have been previously Eased)
			// There may be undesired jumps in the value of Normal when jumping back and forth between
			// multiple iterations of this task in rapid succession?

			var start = source.Value;
			var end   = target.Value;
			var i     = 0.0f;
			var rate  = 1.0f / seconds;

			if (useScaledTime)
			{
				while (i < 1.0f)
				{
					i += Time.deltaTime * rate;

					var e = easingCurve.Evaluate(i);

					source.Value = Mathf.Lerp(start,
					                          end,
					                          e);
					
					await UniTask.Yield();
				}
			}
			else
			{
				while (i < 1.0f)
				{
					i += Time.unscaledDeltaTime * rate;

					var e = easingCurve.Evaluate(i);

					source.Value = Mathf.Lerp(start,
					                          end,
					                          e);
					
					await UniTask.Yield();
				}
			}

			return scope;
		}

	}

}
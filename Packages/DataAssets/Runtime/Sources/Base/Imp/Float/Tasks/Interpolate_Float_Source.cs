using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.DataSources;
using Vanilla.MetaScript;
using Vanilla.TypeMenu;

namespace Vanilla.DataAssets
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

		[SerializeReference]
		[TypeMenu("magenta")]
		public FloatSource speed = new DirectFloatSource()
		                           {
			                           Value = 1.0f
		                           };

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


		protected override string CreateAutoName() => $"Set [{(source is AssetFloatSource s && s.Asset != null ? s.Asset.name : source.GetType().Name)}] to [{(target is AssetFloatSource t && t.Asset != null ? t.Asset.name : target.GetType().Name)}]";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return scope;

			if (source == null ||
			    target == null) return scope;

			// Because we start from the current value of set.Normal (which may have been previously Eased)
			// There may be undesired jumps in the value of Normal when jumping back and forth between
			// multiple iterations of this task in rapid succession?

			var i    = source.Value;
			var t    = target.Value;
			var rate = speed.Value;


			if (i < t)
			{
				if (useScaledTime)
				{
					while (i < t)
					{
						if (scope.Cancelled) return scope;

						i += Time.deltaTime * rate;

						source.Value = easingCurve.Evaluate(i);

						await UniTask.Yield();
					}

					source.Value = t;
				}
				else
				{
					while (i < t)
					{
						if (scope.Cancelled) return scope;

						i += Time.unscaledDeltaTime * rate;

						source.Value = easingCurve.Evaluate(i);

						await UniTask.Yield();
					}

					source.Value = t;
				}
			}
			else if (i > t)
			{
				if (useScaledTime)
				{
					while (i > t)
					{
						if (scope.Cancelled) return scope;

						i -= Time.deltaTime * rate;

						source.Value = easingCurve.Evaluate(i);

						await UniTask.Yield();
					}

					source.Value = t;
				}
				else
				{
					while (i > t)
					{
						if (scope.Cancelled) return scope;

						i -= Time.unscaledDeltaTime * rate;

						source.Value = easingCurve.Evaluate(i);

						await UniTask.Yield();
					}

					source.Value = t;
				}

			}

			return scope;
		}

	}

}
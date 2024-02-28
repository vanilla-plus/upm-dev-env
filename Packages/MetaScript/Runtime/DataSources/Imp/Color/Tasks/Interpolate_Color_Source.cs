using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.MetaScript;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	public class Interpolate_Color_Source : MetaTask
	{

		[SerializeReference]
		[TypeMenu("red")]
		public ColorSource targetSource;

		[SerializeReference]
		[TypeMenu("red")]
		public ColorSource fromSource;
		
		[SerializeReference]
		[TypeMenu("red")]
		public ColorSource toSource;

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

		protected override bool CanAutoName() => targetSource != null && fromSource != null && toSource != null;


		protected override string CreateAutoName() => $"Interpolate [{(targetSource is AssetColorSource s && s.Asset != null ? s.Asset.name : targetSource.GetType().Name)}] from [{(fromSource is AssetColorSource from && from.Asset != null ? from.Asset.name : fromSource.GetType().Name)}] to [{(toSource is AssetColorSource to && to.Asset != null ? to.Asset.name : toSource.GetType().Name)}]";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return scope;

			if (targetSource == null ||
			    fromSource   == null ||
			    toSource     == null) return scope;

			// Because we start from the current value of set.Normal (which may have been previously Eased)
			// There may be undesired jumps in the value of Normal when jumping back and forth between
			// multiple iterations of this task in rapid succession?

			var i = 0.0f;

			var rate = speed.Value;

			var from = fromSource.Value;
			var to   = toSource.Value;

			if (useScaledTime)
			{
				while (i < 1.0f)
				{
					i += Time.deltaTime * rate;

					targetSource.Value = Color.Lerp(from,
					                                to,
					                                easingCurve.Evaluate(i));
					
					await UniTask.Yield();
				}
			}
			else
			{
				while (i < 1.0f)
				{
					i += Time.unscaledDeltaTime * rate;

					targetSource.Value = Color.Lerp(from,
					                                to,
					                                easingCurve.Evaluate(i));
					
					await UniTask.Yield();
				}
			}

			return scope;
		}

	}

}
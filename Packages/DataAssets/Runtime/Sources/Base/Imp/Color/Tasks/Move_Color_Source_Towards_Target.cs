using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.DataSources;
using Vanilla.MetaScript;
using Vanilla.TypeMenu;

namespace Vanilla.DataAssets
{

	[Serializable]
	public class Move_Color_Source_Towards_Target : MetaTask
	{

		[SerializeReference]
		[TypeMenu("red")]
		public ColorSource targetSource;

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

		protected override bool CanAutoName() => targetSource != null && toSource != null;


		protected override string CreateAutoName() => $"Change [{(targetSource is AssetColorSource s && s.Asset != null ? s.Asset.name : targetSource.GetType().Name)}] to [{(toSource is AssetColorSource t && t.Asset != null ? t.Asset.name : toSource.GetType().Name)}] over time";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return scope;

			if (targetSource == null ||
			    toSource == null) return scope;

			var rate = speed.Value;

			var from = targetSource.Value;
			var to   = toSource.Value;

			var delta = (to - from) * rate;

			if (useScaledTime)
			{
				while (from.r < to.r ||
				       from.g < to.g ||
				       from.b < to.b ||
				       from.a < to.a)
				{
					var deltaTimeScaled = delta * Time.deltaTime;

					if (from.r < to.r) from.r += deltaTimeScaled.r;
					if (from.g < to.g) from.g += deltaTimeScaled.g;
					if (from.b < to.b) from.b += deltaTimeScaled.b;
					if (from.a < to.a) from.a += deltaTimeScaled.a;

					targetSource.Value = from;

					await UniTask.Yield();
				}
			}
			else
			{
				while (from.r < to.r ||
				       from.g < to.g ||
				       from.b < to.b ||
				       from.a < to.a)
				{
					var deltaTimeScaled = delta * Time.unscaledDeltaTime;

					if (from.r < to.r) from.r += deltaTimeScaled.r;
					if (from.g < to.g) from.g += deltaTimeScaled.g;
					if (from.b < to.b) from.b += deltaTimeScaled.b;
					if (from.a < to.a) from.a += deltaTimeScaled.a;

					targetSource.Value = from;

					await UniTask.Yield();
				}
			}

			return scope;
		}

	}

}
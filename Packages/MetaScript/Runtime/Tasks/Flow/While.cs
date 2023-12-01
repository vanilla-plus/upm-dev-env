using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public abstract class While : MetaTaskSet
	{

		public abstract bool Evaluate();

		protected override async UniTask<Scope> _Run(Scope scope)
		{
			const int circuitBreakerMax = 100;

			var       circuitBreaker    = 0;
			var       currentFrame      = Time.frameCount;

			while (Evaluate())
			{
				foreach (var task in _tasks)
				{
					if (scope.Cancelled) return scope;

					if (task != null) await task.Run(scope);

					// If we find ourselves stuck on the same frame for over 100 task executions,
					// something has probably gone horribly wrong. Probably...
					if (Time.frameCount == currentFrame)
					{
						if (++circuitBreaker < circuitBreakerMax) continue;
						
						#if debug
						Debug.LogError("Circuit-breaker tripped!");
						#endif

						return scope;
					}

					circuitBreaker = 0;
					currentFrame   = Time.frameCount;
				}
			}

			return scope;
		}

	}

}
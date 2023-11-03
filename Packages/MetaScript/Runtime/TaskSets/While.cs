using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.TaskSets
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
			
			var newScope = new Scope(scope, Name, GetType().Name);

			while (Evaluate())
			{
				foreach (var task in _tasks)
				{
					if (newScope.Cancelled) return scope;

					if (task != null) await task.Run(newScope);

					// If we find ourselves stuck on the same frame for over 100 task executions,
					// something has probably gone horribly wrong. Probably...
					if (Time.frameCount == currentFrame)
					{
						circuitBreaker++;

						if (circuitBreaker >= circuitBreakerMax)
						{
							#if debug
							Debug.LogError("Circuit-breaker tripped!");
							#endif

							newScope.Cancel();
			
							newScope.Dispose();

							return scope;
						}
					}
					else
					{
						circuitBreaker = 0;
						currentFrame   = Time.frameCount;
					}
				}
			}
			
			newScope.Cancel();
			
			newScope.Dispose();

			return scope;
		}

	}

}
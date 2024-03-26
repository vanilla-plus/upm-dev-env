using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public abstract class While_Base : MetaTaskSet
	{

		public abstract bool Evaluate();

		protected override async UniTask<Scope> _Run(Scope scope)
		{
			var s = scope;
			
			const int circuitBreakerMax = 100;

			var       circuitBreaker    = 0;
			var       currentFrame      = Time.frameCount;

			while (Evaluate())
			{
				foreach (var task in _tasks)
				{
					if (s.Cancelled) return s; // This was working fine - just in case the below gives you grief
//					if (s.Cancelled) return s.parent;

					if (task != null)
					{
						s = await task.Run(s);
					}

					// If we find ourselves stuck on the same frame for over 100 task executions,
					// something has probably gone horribly wrong. Probably...
					if (Time.frameCount == currentFrame)
					{
						if (++circuitBreaker < circuitBreakerMax) continue;
						
						#if debug
						Debug.LogError("Circuit-breaker tripped!");
						#endif

						return s;
					}

					circuitBreaker = 0;
					currentFrame   = Time.frameCount;
				}
			}

			return s;
//			return s.GetLastActiveScope();
		}

	}

}
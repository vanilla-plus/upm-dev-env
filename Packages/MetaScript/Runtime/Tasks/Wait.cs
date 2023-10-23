using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Wait : MetaTask
	{

		protected override bool CanAutoName() => true;

		protected override string CreateAutoName() => $"Wait for {secondsToTake} seconds";

		public float secondsToTake = 1.0f;
		
		protected override async UniTask<Scope> _Run(Scope scope)
		{
			var timeRemaining = secondsToTake;

			while (timeRemaining > 0.0f)
			{
				if (scope.Cancelled) return scope;

				timeRemaining -= Time.deltaTime;
				
				await UniTask.Yield();
			}

			return scope;
		}

	}

}
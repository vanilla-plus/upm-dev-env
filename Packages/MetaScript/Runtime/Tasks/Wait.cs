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
		
		protected override async UniTask<ExecutionTrace> _Run(ExecutionTrace trace)
		{
			var timeRemaining = secondsToTake;

			while (timeRemaining > 0.0f)
			{
				if (trace.Cancelled) return trace;
//				if (trace.HasBeenCancelled(this)) return trace;

				timeRemaining -= Time.deltaTime;
				
				await UniTask.Yield();
			}

			return trace;
		}

	}

}
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
		
		protected override async UniTask<Tracer> _Run(Tracer tracer)
		{
			var timeRemaining = secondsToTake;

			while (timeRemaining > 0.0f)
			{
				if (tracer.HasBeenCancelled(this)) return tracer;

				timeRemaining -= Time.deltaTime;
				
				await UniTask.Yield();
			}

			return tracer;
		}

	}

}
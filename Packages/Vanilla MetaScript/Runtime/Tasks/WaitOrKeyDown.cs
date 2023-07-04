using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class WaitOrKeyDown : MetaTask
	{

		protected override bool CanAutoName() => true;

		protected override string CreateAutoName() => $"Wait for {secondsToTake} seconds or {key} key press";
		
		[SerializeField]
		public float secondsToTake = 1.0f;
		
		[SerializeField]
		public KeyCode key = KeyCode.Alpha1;
		
		protected override async UniTask<Tracer> _Run(Tracer tracer)
		{
			var timeRemaining = secondsToTake;

			while (timeRemaining > 0.0f && !Input.GetKeyDown(key))
			{
				if (tracer.Cancelled(this)) return tracer;

				timeRemaining -= Time.deltaTime;
				
				await UniTask.Yield();
			}

			return tracer;
		}

	}

}
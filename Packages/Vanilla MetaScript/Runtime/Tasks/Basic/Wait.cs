using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Wait : MetaTask
	{

		protected override bool CanDescribe() => true;

		protected override string DescribeTask() => $"Wait for {secondsToTake} seconds";

		public float secondsToTake = 1.0f;
		
		protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource)
		{
			var timeRemaining = secondsToTake;

			while (timeRemaining > 0.0f)
			{
//				LogTaskIdentity();
				
//				if (cancellationTokenSource == null || cancellationTokenSource.IsCancellationRequested)
//				{
//					Debug.LogWarning("SNERP");

//					return;
//				}

//				cancellationTokenSource.IsDisposed();

				cancellationTokenSource.Token.ThrowIfCancellationRequested();

				timeRemaining -= Time.deltaTime;
				
				await UniTask.Yield();
			}
		}

	}

}
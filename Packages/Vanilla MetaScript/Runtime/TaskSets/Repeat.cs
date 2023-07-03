#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class Repeat : MetaTaskSet
	{

		[Range(1, 100)]
		[SerializeField]
		public int iterations = 1;

		protected override string DescribeTask() => $"Repeat tasks [{iterations}] times";


		protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource)
		{
			var iteration = 0;

			while (iteration < iterations)
			{
//				if (cancellationTokenSource.IsCancellationRequested) break;
//				cancellationTokenSource.Token.ThrowIfCancellationRequested();

				iteration++;

				#if debug
//				if (!cancellationTokenSource.IsCancellationRequested) Debug.Log($"Beginning iteration [{iteration}]");
				if (!cancellationTokenSource.IsCancellationRequested) LogIteration(iteration);
				#endif

				foreach (var task in _tasks)
				{
//					if (cancellationTokenSource.IsCancellationRequested) break;
					cancellationTokenSource.Token.ThrowIfCancellationRequested();
					
					await task.Run(cancellationTokenSource);
				}
			}
		}
		
		//    Begun        
		public void LogIteration(int i) => Debug.Log($"{Time.frameCount:0000000}    {GetType().Name,LongestTaskName}    i:{i:0000}       {ExecutionType,LongestExecutionType}    {Name}");


	}

}
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

		protected override string CreateAutoName() => $"Repeat the following [{iterations}] times:";


		protected override async UniTask<ExecutionTrace> _Run(ExecutionTrace trace)
		{
			var iteration = 0;
			
			while (iteration++ < iterations)
			{
//				iteration++;

//				#if debug
//				LogIteration(trace, iteration);
//				#endif

				foreach (var task in _tasks)
				{
					if (trace.Cancelled) return trace;
//					if (trace.HasBeenCancelled(this)) return trace;

					await task.Run(trace);
				}
			}

			return trace;
		}
		
//		public void LogIteration(ExecutionTrace trace, int i) => Debug.Log($"{Time.frameCount:0000000}    {trace.scope.ActiveTasks}    {GetType().Name,LongestTaskName}    i:{i:0000}       {executionOptions.ToString()}    {Name}");


	}

}
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


		protected override async UniTask<Tracer> _Run(Tracer tracer)
		{
			var iteration = 0;
			
			while (iteration < iterations)
			{
//				if (tracer.Cancelled(this)) break;

				iteration++;

				#if debug
				LogIteration(tracer, iteration);
				#endif

				foreach (var task in _tasks)
				{
					if (tracer.Cancelled(this)) return tracer;

					await task.Run(tracer);
				}
			}

			return tracer;
		}
		
		public void LogIteration(Tracer tracer, int i) => Debug.Log($"{Time.frameCount:0000000}    {tracer.Depth}    {GetType().Name,LongestTaskName}    i:{i:0000}       {ExecutionType,LongestExecutionType}    {Name}");


	}

}
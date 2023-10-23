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


		protected override async UniTask<Scope> _Run(Scope scope)
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
					if (scope.Cancelled) return scope;
//					if (trace.HasBeenCancelled(this)) return trace;

					await task.Run(scope);
				}
			}

			return scope;
		}
		
//		public void LogIteration(Scope scope, int i) => Debug.Log($"{Time.frameCount:0000000}    {trace.scope.ActiveTasks}    {GetType().Name,LongestTaskName}    i:{i:0000}       {executionOptions.ToString()}    {Name}");


	}

}
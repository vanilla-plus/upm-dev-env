using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.DotNetExtensions;

using Random = UnityEngine.Random;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class TaskSet_Random : TaskSet
	{

		[SerializeField] // Convert to int socket
		public int numberOfRandomTasksToRun = 1;

		[SerializeField] // Convert to bool socket
		[Tooltip("If true, extra work will be performed to ensure that the same task isn't provided more than once.")]
		public bool uniqueTasks = true;

		public override    string GetDescription()   => $"Run [{numberOfRandomTasksToRun}] {(uniqueTasks ? "[unique] " : string.Empty)}tasks at random";

		[ContextMenu(itemName: "Run")]
		public override async UniTask Run()
		{
			if (!uniqueTasks)
			{
				var tasksToFinish = numberOfRandomTasksToRun;

				while (tasksToFinish > 0)
				{
					var currentTaskIndex = Random.Range(minInclusive: 0,
					                                    maxExclusive: taskRunners.Length);

					#if DEBUG_METASCRIPT
					Debug.Log($"Choosing a random task... [{currentTaskIndex}]");
					#endif

					await taskRunners[currentTaskIndex].Run();

					tasksToFinish--;
				}
			}
			else
			{
				var taskIndices = new int[taskRunners.Length];

				for (var i = 0;
				     i < taskIndices.Length;
				     i++)
				{
					taskIndices[i] = i;
				}
				
				taskIndices.Shuffle();

				#if DEBUG_METASCRIPT
				var l = "The random tasks will be:\n";
				
				for (var i = 0;
				     i < numberOfRandomTasksToRun;
				     i++)
				{
					l += $"\t- {taskRunners[taskIndices[i]].task.GetDescription()}\n";
				}
				
//				Debug.Log(taskIndices.Aggregate(seed:"The random tasks will be:\n", func: (current,
//				                                                                           i) => $"\t{current}- {taskRunners[i].task.GetDescription()}\n"));

				Debug.Log(l);
				#endif

				for (var i = 0;
				     i < numberOfRandomTasksToRun;
				     i++)
				{
					await taskRunners[taskIndices[i]].Run();
				}
				
				
//				// This first step is a bit complicated to accomplish without generating any garbage.
//				// The short explanation is that we're constructing the randomized non-repeating collection of tasks just once at the start.
//				// That way, all that's left is to pop task indices out of the stack until its empty.
//				var taskStack = new Stack<int>(numberOfRandomTasksToRun);
//				
//				// The plan is that the following local vars get thrown out immediately afterwards because of the scope..?
//				// We only want the hashSet for a split second to guard against duplicates.
//				// Does this actually work?
//				if (true)
//				{
//					var totalTaskCount = taskRunners.Length - 1;
//					var hashSet        = new HashSet<int>();
//					var i              = 0;
//
//					while (i < numberOfRandomTasksToRun)
//					{
//						var r = Random.Range(minInclusive: 0,
//						                     maxExclusive: totalTaskCount);
//
//						if (!hashSet.Add(item: r)) continue;
//
//						taskStack.Push(item: r);
//							
//						i++;
//					}
//				}
//				
//
////				var tasksToFinish = numberOfRandomTasksToRun;
////
////				while (tasksToFinish > 0)
////				{
////					var currentTaskIndex = Random.Range(minInclusive: 0,
////					                                    maxExclusive: taskRunners.Length - 1);
////
////					while (completedTasks.Contains(item: currentTaskIndex))
////					{
////						currentTaskIndex = Random.Range(minInclusive: 0,
////						                                maxExclusive: taskRunners.Length - 1);
////					}
////
////					#if DEBUG_METASCRIPT
////					Debug.Log($"Choosing a random task... [{currentTaskIndex}]");
////					#endif
////
////					await taskRunners[currentTaskIndex].Run();
////
//////					completedTasks.Add(taskRunners.IndexOf(currentTask)));
////
////					completedTasks.Add(currentTaskIndex);
////
////					tasksToFinish--;
////				}
//				
			}
		}

	}

}
using System;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class Random : MetaTaskSet
	{

		protected override string DescribeTask() => "Run one of the tasks at random";


		protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource) => await Tasks[UnityEngine.Random.Range(0,
		                                                                                                                               _tasks.Length)]
			                                                                                          .Run(cancellationTokenSource);

	}

}
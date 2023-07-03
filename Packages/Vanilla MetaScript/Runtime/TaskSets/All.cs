using System;
using System.Linq;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class All : MetaTaskSet
	{

		protected override string DescribeTask() => Tasks.Aggregate(seed: "Proceed when all of the following complete ",
		                                                            func: (current,
		                                                                   t) => current[..^1] + $", {t.Name}");


		protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource) => await UniTask.WhenAll(tasks: Enumerable.Select(source: _tasks,
		                                                                                                                                         selector: t => t.Run(cancellationTokenSource)));

	}

}
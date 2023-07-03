using System;
using System.Linq;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class Any : MetaTaskSet
	{

		protected override string DescribeTask() => Tasks.Aggregate(seed: "Proceed when any of the following complete ",
		                                                            func: (current,
		                                                                   t) => current[..^1] + $", {t.Name}");


		protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource) => await UniTask.WhenAny(tasks: Enumerable.Select(source: _tasks,
		                                                                                                                                         selector: t => t.Run(cancellationTokenSource)));

	}

}
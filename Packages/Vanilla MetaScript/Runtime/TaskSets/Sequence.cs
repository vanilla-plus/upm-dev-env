using System;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class Sequence : MetaTaskSet
	{

		protected override string DescribeTask() => $"Run tasks in order [{Tasks[0].Name}...]";


		protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource)
		{
			foreach (var task in _tasks)
			{
//				if (cancellationTokenSource.IsCancellationRequested) break;
				cancellationTokenSource.Token.ThrowIfCancellationRequested();
				
				await task.Run(cancellationTokenSource);
			}
		}

	}

}
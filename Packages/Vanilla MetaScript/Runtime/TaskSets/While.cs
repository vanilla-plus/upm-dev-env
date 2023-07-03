using System;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public abstract class While : MetaTaskSet
	{

		public abstract bool Evaluate();

		protected override string DescribeTask() => "Repeat tasks while Evaluate returns false";


		protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource)
		{
			while (!Evaluate())
			{
				foreach (var task in _tasks)
				{
//					if (cancellationTokenSource.IsCancellationRequested) break;

					cancellationTokenSource.Token.ThrowIfCancellationRequested();
					
					await task.Run(cancellationTokenSource);
				}
			}
		}

	}

}
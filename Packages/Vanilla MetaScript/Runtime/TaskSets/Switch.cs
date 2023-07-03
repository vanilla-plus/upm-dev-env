using System;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public abstract class Switch : MetaTaskSet
	{

		public abstract int Evaluate();


		protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource)
		{
			var result = Evaluate();

			var task = Tasks[result];

			await task.Run(cancellationTokenSource);
		}

	}

}
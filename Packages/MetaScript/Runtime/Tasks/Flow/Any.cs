using System;
using System.Linq;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public class Any : MetaTaskSet
	{

		protected override string CreateAutoName() => "Proceed when any of the following complete:";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return scope;

			var tasksToRun = _tasks.Where(t => t.taskOptions.HasFlag(TaskOptions.Run));

			await UniTask.WhenAny(tasks: tasksToRun.Select(t => t.Run(scope)));

			return scope;
		}

	}

}
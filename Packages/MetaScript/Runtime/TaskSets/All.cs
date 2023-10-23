using System;
using System.Linq;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class All : MetaTaskSet
	{

		protected override string CreateAutoName() => "Proceed when all of the following complete:";

		protected override async UniTask<ExecutionTrace> _Run(ExecutionTrace tracer)
		{
			var tempScope = new ExecutionScope(tracer.scope);

			var tempTrace = tempScope.GetNewTrace();
			
			await UniTask.WhenAll(tasks: Enumerable.Select(source: _tasks,
			                                               selector: t => t.Run(tempTrace)));

			tempScope.Cancel();
			
			return tracer;
		}

	}

}
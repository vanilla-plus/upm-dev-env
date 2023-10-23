using System;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class Sequence : MetaTaskSet
	{

		protected override string CreateAutoName() => "Run the following in order:";


		protected override async UniTask<ExecutionTrace> _Run(ExecutionTrace trace)
		{
			var tempScope = new ExecutionScope(trace.scope);

			var tempTrace = tempScope.GetNewTrace();
			
			foreach (var task in _tasks)
			{
				if (tempTrace.Cancelled) return trace;
				
				await task.Run(tempTrace);
			}

			return trace;
		}

	}

}
using System;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public abstract class While : MetaTaskSet
	{

		public abstract bool Evaluate();

//		protected override string CreateAutoName() => "Repeat the following tasks while...";

		protected override async UniTask<ExecutionTrace> _Run(ExecutionTrace trace)
		{
			while (Evaluate())
			{
				foreach (var task in _tasks)
				{
					if (trace.Cancelled) return trace;
//					if (tracer.HasBeenCancelled(this)) return tracer;

					await task.Run(trace);
				}
			}

			return trace;
		}

	}

}
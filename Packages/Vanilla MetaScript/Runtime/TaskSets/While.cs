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


		protected override async UniTask<Tracer> _Run(Tracer tracer)
		{
			while (!Evaluate())
			{
				foreach (var task in _tasks)
				{
					if (tracer.Cancelled(this)) return tracer;

					await task.Run(tracer);
				}
			}

			return tracer;
		}

	}

}
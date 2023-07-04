using System;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class Sequence : MetaTaskSet
	{

		protected override string DescribeTask() => $"Run tasks in order [{Tasks[0].Name}...]";


		protected override async UniTask<Tracer> _Run(Tracer tracer)
		{
			foreach (var task in _tasks)
			{
				if (tracer.Cancelled(this)) break;
				
				await task.Run(tracer);
			}

			return tracer;
		}

	}

}
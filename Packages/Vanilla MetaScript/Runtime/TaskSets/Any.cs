using System;
using System.Linq;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class Any : MetaTaskSet
	{

//		protected override string CreateAutoName() => Tasks.Aggregate(seed: "Proceed when any of the following complete ",
//		                                                            func: (current,
//		                                                                   t) => current[..^1] + $", {t.Name}");

		protected override string CreateAutoName() => "Proceed when any of the following complete:";


		protected override async UniTask<Tracer> _Run(Tracer tracer)
		{
//			tracer.Depth++;

			await UniTask.WhenAny(tasks: Enumerable.Select(source: _tasks,
			                                               selector: t => t.Run(tracer)));

//			tracer.Depth--;

			return tracer;
		}

	}

}
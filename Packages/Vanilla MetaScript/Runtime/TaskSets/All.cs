using System;
using System.Linq;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class All : MetaTaskSet
	{

//		protected override string CreateAutoName() => Tasks.Aggregate(seed: "Proceed when all of the following complete ",
//		                                                            func: (current,
//		                                                                   t) => current[..^1] + $", {t.Name}");

		protected override string CreateAutoName() => "Proceed when all of the following complete:";

		protected override async UniTask<Tracer> _Run(Tracer tracer)
		{
//			tracer.Depth++;
			
			await UniTask.WhenAll(tasks: Enumerable.Select(source: _tasks,
			                                               selector: t => t.Run(tracer)));

//			tracer.Depth--;

			return tracer;
		}

	}

}
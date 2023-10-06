using System;
using System.Linq;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class Any : MetaTaskSet
	{

		protected override string CreateAutoName() => "Proceed when any of the following complete:";


		protected override async UniTask<Tracer> _Run(Tracer tracer)
		{
			await UniTask.WhenAny(tasks: Enumerable.Select(source: _tasks,
			                                               selector: t => t.Run(tracer)));

			return tracer;
		}

	}

}
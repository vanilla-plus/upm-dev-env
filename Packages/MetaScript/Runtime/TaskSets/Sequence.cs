using System;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class Sequence : MetaTaskSet
	{

		protected override string CreateAutoName() => "Run the following in order:";


		protected override async UniTask<Tracer> _Run(Tracer tracer)
		{
			foreach (var task in _tasks)
			{
				if (tracer.HasBeenCancelled(this)) break;
				
				await task.Run(tracer);
			}

			return tracer;
		}

	}

}
using System;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public abstract class While : MetaTaskSet
	{

		public abstract bool Evaluate();

//		protected override string CreateAutoName() => "Repeat the following tasks while...";

		protected override async UniTask<Tracer> _Run(Tracer tracer)
		{
			while (Evaluate())
			{
				foreach (var task in _tasks)
				{
					if (tracer.HasBeenCancelled(this)) return tracer;

					await task.Run(tracer);
				}
			}

			return tracer;
		}

	}

}
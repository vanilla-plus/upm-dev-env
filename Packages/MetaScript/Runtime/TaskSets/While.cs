using System;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public abstract class While : MetaTaskSet
	{

		public abstract bool Evaluate();

//		protected override string CreateAutoName() => "Repeat the following tasks while...";

		protected override async UniTask<Scope> _Run(Scope scope)
		{
			while (Evaluate())
			{
				foreach (var task in _tasks)
				{
					if (scope.Cancelled) return scope;
//					if (tracer.HasBeenCancelled(this)) return tracer;

					await task.Run(scope);
				}
			}

			return scope;
		}

	}

}
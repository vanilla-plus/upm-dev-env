using System;
using System.Linq;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class All : MetaTaskSet
	{

		protected override string CreateAutoName() => "Proceed when all of the following complete:";

		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return scope;

//			var newScope = new Scope(scope, Name, GetType().Name);
//
//			await UniTask.WhenAll(tasks: Enumerable.Select(source: _tasks,
//			                                               selector: t => t.Run(newScope)));
//
//			newScope.Cancel();
//			
//			newScope.Dispose();

			await UniTask.WhenAll(tasks: Enumerable.Select(source: _tasks,
			                                               selector: t => t.Run(scope)));
			
			return scope;
		}

	}

}
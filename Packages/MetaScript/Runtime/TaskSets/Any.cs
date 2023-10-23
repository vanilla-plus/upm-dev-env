using System;
using System.Linq;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class Any : MetaTaskSet
	{

		protected override string CreateAutoName() => "Proceed when any of the following complete:";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return scope;

			var newScope = new Scope(scope, Name, GetType().Name);

			await UniTask.WhenAny(tasks: Enumerable.Select(source: _tasks,
			                                               selector: t => t.Run(newScope)));

			newScope.Cancel();
			
			newScope.Dispose();
			
			return scope; // Make sure to return the original scope, not the new one!
		}

	}

}
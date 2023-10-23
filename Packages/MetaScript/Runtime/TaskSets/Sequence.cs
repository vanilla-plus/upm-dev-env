using System;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class Sequence : MetaTaskSet
	{

		protected override string CreateAutoName() => "Run the following in order:";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return scope; // It's important to guard against scopes that are already cancelled.

			var newScope = new Scope(scope, Name, GetType().Name);

			foreach (var task in _tasks)
			{
				if (newScope.Cancelled) return scope;
				
				await task.Run(newScope);
			}

			newScope.Cancel();
			
			newScope.Dispose();

			return scope;
		}

	}

}
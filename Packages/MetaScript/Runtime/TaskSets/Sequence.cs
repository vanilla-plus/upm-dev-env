using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class Sequence : MetaTaskSet
	{

		protected override string CreateAutoName() => "Run the following in order:";

		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return scope; // It's important to guard against scopes that are already cancelled.

//			var newScope = new Scope(parent: scope,
//			                         taskName: Name,
//			                         taskType: GetType().Name);
//
//			foreach (var task in _tasks)
//			{
//				if (newScope.Cancelled) return scope;
//
//				if (task != null) await task.Run(newScope);
//			}
//
//			newScope.Cancel();
//
//			newScope.Dispose();

			foreach (var task in _tasks)
			{
				if (scope.Cancelled) return scope;

				if (task != null) await task.Run(scope);
			}

			return scope;
		}

	}

}
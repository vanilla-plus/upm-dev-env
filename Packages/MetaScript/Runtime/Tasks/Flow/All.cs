using System;
using System.Linq;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public class All : MetaTaskSet
	{

		protected override string CreateAutoName() => "Proceed when all of the following complete:";

		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return scope;

			await UniTask.WhenAll(tasks: Enumerable.Select(source: _tasks,
			                                               selector: t => t.Run(scope)));
			
			return scope;
		}

	}

}
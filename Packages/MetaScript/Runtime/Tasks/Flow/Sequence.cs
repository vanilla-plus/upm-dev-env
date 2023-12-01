using System;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public class Sequence : MetaTaskSet
	{

		protected override string CreateAutoName() => "Run the following in order:";

		protected override async UniTask<Scope> _Run(Scope scope)
		{
			foreach (var task in _tasks)
			{
				if (scope.Cancelled) return scope;

				if (task != null) await task.Run(scope);
			}

			return scope;
		}

	}

}
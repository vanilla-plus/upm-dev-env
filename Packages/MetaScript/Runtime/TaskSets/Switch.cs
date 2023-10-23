using System;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public abstract class Switch : MetaTaskSet
	{

		public abstract int Evaluate();


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			var result = Evaluate();

			var task = Tasks[result];

			await task.Run(scope);

			return scope;
		}

	}

}
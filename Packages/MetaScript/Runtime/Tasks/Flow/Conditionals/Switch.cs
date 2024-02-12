using System;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public abstract class Switch : MetaTaskSet
	{

		public abstract int Evaluate();


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			var result = Evaluate();

			var task = Tasks[result];

			if (task != null) await task.Run(scope);

			return scope;
		}

	}

}
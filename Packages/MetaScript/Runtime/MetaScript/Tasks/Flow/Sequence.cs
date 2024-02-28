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
			var s = scope;
			
			foreach (var task in _tasks)
			{
				if (s.Cancelled) return s;

				if (task != null)
				{
					s = await task.Run(s);
				}
			}

			return s;
		}

	}

}
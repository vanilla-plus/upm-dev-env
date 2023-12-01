using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public class Repeat : MetaTaskSet
	{

		[Range(1, 100)]
		[SerializeField]
		public int iterations = 1;

		protected override string CreateAutoName() => $"Repeat the following [{iterations}] times:";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			var iteration = 0;
			
			while (iteration++ < iterations)
			{
				foreach (var task in _tasks)
				{
					if (scope.Cancelled) return scope;

					if (task != null) await task.Run(scope);
				}
			}

			return scope;
		}
		
	}

}
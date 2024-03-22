using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public class For_Loop : MetaTaskSet
	{

//		[Range(1, 100)]
//		[SerializeField]
//		public int iterations = 1;
//		public  iterations;

		[SerializeReference]
		[TypeMenu("red")]
		public IntSource Iterations;

		protected override string CreateAutoName() => $"Repeat the following [{Iterations}] times:";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			var iteration = 0;
			
			while (iteration++ < Iterations.Value)
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
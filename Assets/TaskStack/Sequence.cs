using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace MagicalProject
{

	[Serializable]
	public class Sequence : SomeTask
	{

		[SerializeReference]
		[TypeMenu("green")]
		public SomeTask[] tasks = Array.Empty<SomeTask>();


		public override void OnValidate()
		{
			#if UNITY_EDITOR
			base.OnValidate();

			foreach (var t in tasks)
			{
				t?.OnValidate();
			}
			#endif
		}


		protected override string AutoName=> $"Run this sequence of tasks";


		public override UniTask<Scope> Run(Scope scope)
		{
			for (var i = tasks.Length - 1;
			     i >= 0;
			     i--)
			{
				scope.Add(tasks[i]);
			}

			return default;
		}

	}

}
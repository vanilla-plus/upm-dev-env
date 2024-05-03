using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public class Wait_Until_Bool : MetaTask
	{

		[TypeMenu("red")]
		[SerializeReference]
		public BoolSource condition;
		
		protected override bool Valid => condition != null;

		protected override string CreateAutoName() => $"Wait until [{(condition == null ? "null" : condition)}]";

		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (condition == null)
			{
				Debug.LogError("Wait_Until task has a null condition!");
				
				return scope;
			}

			while (condition.Value == false)
			{
				if (scope.Cancelled) return scope;

				await UniTask.Yield();
			}

			return scope;
		}

	}

}
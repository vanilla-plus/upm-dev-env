using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public class Invoke_MetaAction : MetaTask
	{

		[SerializeField] public MetaAction_Base metaAction;

		protected override bool Valid => metaAction != null;

		protected override string CreateAutoName() => $"Invoke [{metaAction.name}]";


		protected override UniTask<Scope> _Run(Scope scope)
		{
			metaAction.Invoke();

			return UniTask.FromResult(scope);
		}

	}

}
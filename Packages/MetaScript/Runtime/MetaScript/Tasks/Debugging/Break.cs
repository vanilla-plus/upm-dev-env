using UnityEngine;
using System;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.Debugging
{

	[Serializable]
	public class Break : MetaTask
	{

		protected override bool CanAutoName() => true;

		protected override string CreateAutoName() => "Call Debug.Break";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return scope;

			Debug.Break();

			await UniTask.NextFrame();

			return scope;
		}

	}

}
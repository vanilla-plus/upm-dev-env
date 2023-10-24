using System;
using System.Threading;

using Cysharp.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Quit : MetaTask
	{

		protected override bool CanAutoName() => true;


		protected override string CreateAutoName() => "Quit the entire application";


		protected override UniTask<Scope> _Run(Scope scope)
		{
			#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
			#else
			Application.Quit();
			#endif

			return UniTask.FromResult(scope);
		}

	}

}
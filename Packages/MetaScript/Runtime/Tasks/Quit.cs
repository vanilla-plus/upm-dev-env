using System;
using System.Threading;

using Cysharp.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;

using UnityEngine;
#endif

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Quit : MetaTask
	{

		protected override bool CanAutoName() => true;


		protected override string CreateAutoName() => "Quit the entire application";


		protected override UniTask<ExecutionTrace> _Run(ExecutionTrace trace)
		{
			#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
			#else
			Application.Quit();
			#endif

			return UniTask.FromResult(trace);
		}

	}

}
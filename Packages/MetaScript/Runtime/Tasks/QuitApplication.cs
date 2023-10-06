using System;
using System.Threading;

using Cysharp.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Vanilla.MetaScript
{

	[Serializable]
	public class QuitApplication : MetaTask
	{

		protected override bool CanAutoName() => true;


		protected override string CreateAutoName() => "Quit the entire application";


		protected override UniTask<Tracer> _Run(Tracer tracer)
		{
			#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
			#else
			Application.Quit();
			#endif

			return UniTask.FromResult(tracer);
		}

	}

}
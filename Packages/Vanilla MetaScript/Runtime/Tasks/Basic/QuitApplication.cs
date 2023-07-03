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

		protected override bool CanDescribe() => true;


		protected override string DescribeTask() => "Quit the entire application";


		protected override UniTask _Run(CancellationTokenSource cancellationTokenSource)
		{
			#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
			#else
			Application.Quit();
			#endif

			return default;
		}

	}

}
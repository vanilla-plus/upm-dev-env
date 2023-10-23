using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Log : MetaTask
	{

		protected override bool CanAutoName() => !string.IsNullOrEmpty(Message);

		protected override string CreateAutoName() => $"Print [{Message}] to the console";

		[SerializeField]
		public string Message;


		protected override UniTask<ExecutionTrace> _Run(ExecutionTrace trace)
		{
			Debug.Log(Message);

			return UniTask.FromResult(trace);
		}

	}

}
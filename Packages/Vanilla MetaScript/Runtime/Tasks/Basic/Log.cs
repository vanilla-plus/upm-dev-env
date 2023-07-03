using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Log : MetaTask
	{

		protected override bool CanDescribe() => !string.IsNullOrEmpty(Message);

		protected override string DescribeTask() => $"Print [{Message}] to the console";

		[SerializeField]
		public string Message;


		protected override UniTask _Run(CancellationTokenSource cancellationTokenSource)
		{
			Debug.Log(Message);

			return default;
		}

	}

}
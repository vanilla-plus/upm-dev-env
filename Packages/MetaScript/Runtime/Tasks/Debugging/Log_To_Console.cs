using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.Debugging
{

	[Serializable]
	public class Log_To_Console : MetaTask
	{

		public enum LogType
		{

			Log,
			Warning,
			Error

		}

		[SerializeField]
		public LogType logType = LogType.Log;

		protected override bool CanAutoName() => !string.IsNullOrEmpty(Message);

		protected override string CreateAutoName() => $"Print [{Message}] to the console";

		[SerializeField]
		public string Message;


		protected override UniTask<Scope> _Run(Scope scope)
		{
			
			switch (logType)
			{
				case LogType.Log:
					Debug.Log(Message);

					break;

				case LogType.Warning:
					Debug.LogWarning(Message);

					break;

				case LogType.Error:
					Debug.LogError(Message);

					break;

				default: throw new ArgumentOutOfRangeException();
			}

			return UniTask.FromResult(scope);
		}

	}

}
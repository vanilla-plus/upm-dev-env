using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataSources.Strings;
using Vanilla.TypeMenu;

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

		protected override bool Validate => MessageSource != null;

		protected override string CreateAutoName() => $"Print [{MessageSource}] to the console";

		[TypeMenu("red")]
		[SerializeReference]
		public StringSource MessageSource;
		
		protected override UniTask<Scope> _Run(Scope scope)
		{
			switch (logType)
			{
				case LogType.Log:
					Debug.Log(MessageSource);

					break;

				case LogType.Warning:
					Debug.LogWarning(MessageSource);

					break;

				case LogType.Error:
					Debug.LogError(MessageSource);

					break;

				default: throw new ArgumentOutOfRangeException();
			}

			return UniTask.FromResult(scope);
		}

	}

}
using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Log_To_Console : TaskBase
	{

		public override string GetDescription() => $"Log to the console: \"{message}\"";

		// Convert to string socket
		// This one is weird. Some strings are just a basic message, but lots of logs are often concats of current data values...
		// We might need some sort of like... Log Data Assets task? And it takes a list of Data Assets and logs out their value.ToString()
		[SerializeField] 
		public string message = "Hello";

		public override async UniTask Run() => Debug.Log(message);

	}

}
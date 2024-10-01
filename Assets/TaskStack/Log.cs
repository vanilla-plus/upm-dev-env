using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace MagicalProject
{

	[Serializable]
	public class Log : SomeTask
	{

		[SerializeField]
		public string message;

		protected override string AutoName => $"Log {message}";


		public override UniTask<Scope> Run(Scope scope)
		{
			Debug.Log(message);

			return default;
		}

	}

}
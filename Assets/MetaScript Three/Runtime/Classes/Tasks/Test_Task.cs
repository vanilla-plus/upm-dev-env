using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.DataAssets;

namespace Vanilla.MetaScript.Three
{

	[Serializable]
	public class Test_Task : ITask
	{

		[SerializeField]
		private bool _async;
		public bool async
		{
			get => _async;
			set => _async = value;
		}

		public StringSocket message;

		public void Validate() { }

		public async UniTask Run()
		{
			await UniTask.Yield();

			Debug.Log(message.Get());
		}

	}

}
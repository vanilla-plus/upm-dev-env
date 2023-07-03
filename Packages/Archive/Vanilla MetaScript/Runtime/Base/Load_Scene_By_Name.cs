using System;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Load_Scene_By_Name : TaskBase
	{

		[SerializeField] // Convert to string socket
		public string sceneName;

		[SerializeField] // Convert to float socket
		public float progress;

		[SerializeField]
		public LoadSceneMode loadMode = LoadSceneMode.Additive;

		public override string GetDescription() => $"Load the scene called [{sceneName}]";


		public override async UniTask Run()
		{
			var op = SceneManager.LoadSceneAsync(sceneName: sceneName,
			                                     mode: loadMode);

			while (!op.isDone)
			{
				progress = op.progress;

				await UniTask.Yield();
			}
		}

	}

}
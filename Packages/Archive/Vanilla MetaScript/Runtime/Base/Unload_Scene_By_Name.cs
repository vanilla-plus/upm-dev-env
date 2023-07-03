using System;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Unload_Scene_By_Name : TaskBase
	{

		[SerializeField] // Convert to string socket
		public string sceneName;

		public override string GetDescription() => $"Unload the scene called [{sceneName}]";

		public override async UniTask Run() => await SceneManager.UnloadSceneAsync(sceneName: sceneName);

	}

}
using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	public class Unload_Scene_By_Name : MetaTask
	{

		[SerializeField]
		public string sceneName;

		[SerializeField]
		public UnloadSceneOptions unloadOptions = UnloadSceneOptions.UnloadAllEmbeddedSceneObjects;
		
		[SerializeField]
		public Action<float> OnProgress;
        
		protected override bool CanAutoName() => !string.IsNullOrWhiteSpace(sceneName);


		protected override string CreateAutoName() => $"Load [{sceneName}] scene";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			var op = SceneManager.UnloadSceneAsync(sceneName,
			                                       unloadOptions);

			while (!op.isDone)
			{
				if (scope.Cancelled) return scope;

				OnProgress?.Invoke(op.progress);
                
				await UniTask.Yield();
			}

			return scope;
		}

	}
}
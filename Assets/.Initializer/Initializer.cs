#if DEBUG_VANILLA && INITIALIZER
#define debug
#endif

using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using UnityEngine.SceneManagement;

using Object = UnityEngine.Object;

namespace Vanilla.Initializer
{

	public static class Initializer
	{

		public enum InitializationState
		{

			None,
			InitializatingScene,
			DeinitializingScene

		}

		public static InitializationState initializationState = InitializationState.None;
		
		public static Action<Scene> SceneInitializationBegun;

		public static Action<Scene> SceneInitializationComplete;
		
		public static Action<Scene> SceneDeinitializationBegun;

		public static Action<Scene> SceneDeinitializationComplete;

		// ---------------------------------------------------------------------------------------------------------------------- Hook onto Unity //

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		private static void AfterSceneLoad() => SceneManager.sceneLoaded   += RunInitialize;

		// ----------------------------------------------------------------------------------------------------------------------- Initialization //
		
		private static void RunInitialize(Scene scene,
		                                  LoadSceneMode loadSceneMode) => InitializeScene(scene);

		private static async UniTask InitializeScene(Scene scene)
		{
			
			#if debug
			Debug.Log($"[Initializer] Initialization of scene [{scene.name}] begun");
			#endif
			
			initializationState = InitializationState.InitializatingScene;

			SceneInitializationBegun?.Invoke(obj: scene);

			foreach (var g in scene.GetRootGameObjects())
			{
				foreach (var i in g.GetComponentsInChildren<IInitiable>(includeInactive: true))
				{
					await i.Initialize();
				}
			}

			initializationState = InitializationState.None;

			#if debug
			Debug.Log($"[Initializer] Initialization of scene [{scene.name}] complete.");
			#endif

			SceneInitializationComplete?.Invoke(obj: scene);
			
		}
		
		// -------------------------------------------------------------------------------------------------------------------- De-initialization //


		public static async UniTask RequestUnload(Scene scene)
		{
			
			#if debug
			Debug.Log($"[Initializer] De-initializing of scene [{scene.name}] begun");
			#endif
			
			initializationState = InitializationState.DeinitializingScene;

			SceneDeinitializationBegun?.Invoke(obj: scene);

			foreach (var g in scene.GetRootGameObjects())
			{
				foreach (var i in g.GetComponentsInChildren<IInitiable>(includeInactive: true))
				{
					await i.DeInitialize();
				}
			}

			initializationState = InitializationState.None;

			#if debug
			Debug.Log($"[Initializer] De-initialization of scene [{scene.name}] complete.");
			#endif
			
			SceneDeinitializationComplete?.Invoke(obj: scene);
			
			// SceneLoader.ActuallyUnloadTheScene(scene);
		}

		// ------------------------------------------------------------------------------------------------------------------------ Instantiation //

		public static async UniTask<T> Instantiate<T>(T target)
			where T : Object, IInitiable
		{
			var copy = Object.Instantiate(original: target);

			await copy.Initialize();

			return copy;
		}

	}

}
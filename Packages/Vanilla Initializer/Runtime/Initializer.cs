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
			Initializating,
			Deinitializing

		}

		public static InitializationState state = InitializationState.None;
		
		public static Action<Scene> SceneInitializationBegun;

		public static Action<Scene> SceneInitializationComplete;
		
		public static Action<Scene> SceneDeinitializationBegun;

		public static Action<Scene> SceneDeinitializationComplete;

		// ---------------------------------------------------------------------------------------------------------------------- Hook onto Unity //

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		private static void AfterSceneLoad() => SceneManager.sceneLoaded += RunInitialize;

		// ----------------------------------------------------------------------------------------------------------------------- Initialization //
		
		private static void RunInitialize(Scene scene,
		                                  LoadSceneMode loadSceneMode) => InitializeScene(scene);

		private static async UniTask InitializeScene(Scene scene)
		{
			
			#if debug
			Debug.Log($"[Initializer] Initialization of scene [{scene.name}] begun");
			#endif
			
			state = InitializationState.Initializating;

			SceneInitializationBegun?.Invoke(obj: scene);

			foreach (var g in scene.GetRootGameObjects())
			{
				foreach (var i in g.GetComponentsInChildren<IInitiable>(includeInactive: true))
				{
					#if debug
					Debug.Log($"[Initializer] Running Initialize() for [{g.name}.{i.GetType().Name}]");
					#endif
					
					await i.Initialize();
				}
			}

			state = InitializationState.None;

			#if debug
			Debug.Log($"[Initializer] Initialization of scene [{scene.name}] complete.");
			#endif

			SceneInitializationComplete?.Invoke(obj: scene);
			
		}
		
		// -------------------------------------------------------------------------------------------------------------------- De-initialization //

		public static async UniTask DeInitializeScene(Scene scene)
		{
			#if debug
			Debug.Log($"[Initializer] De-initializing of scene [{scene.name}] begun");
			#endif
			
			state = InitializationState.Deinitializing;

			SceneDeinitializationBegun?.Invoke(obj: scene);

			foreach (var g in scene.GetRootGameObjects())
			{
				foreach (var i in g.GetComponentsInChildren<IInitiable>(includeInactive: true))
				{
					#if debug
					Debug.Log($"[Initializer] Running DeInitialize() for [{g.name}.{i.GetType().Name}]");
					#endif
					
					await i.DeInitialize();
				}
			}

			state = InitializationState.None;

			#if debug
			Debug.Log($"[Initializer] De-initialization of scene [{scene.name}] complete.");
			#endif
			
			SceneDeinitializationComplete?.Invoke(obj: scene);
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
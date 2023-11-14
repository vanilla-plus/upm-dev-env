#undef debug

using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

///*
//static Lifecycle() => Debug.Log(Prefix + "Static Constructor");
//[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)] static void Subs() => Debug.Log(Prefix + "Subsystem Registration");
//[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)] static void AfterAsm() => Debug.Log(Prefix + "AfterAssembliesLoaded");
//[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)] static void BeforeSlash() => Debug.Log(Prefix + "Before Splash");
//[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] static void BeforeScene() => Debug.Log(Prefix + "BeforeScene");
//private void Awake() => Debug.Log(Prefix + "Awake");
//private void OnEnable() => Debug.Log(Prefix + "OnEnable");

//[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)] static void AfterScene() => Debug.Log(Prefix + "AfterSceneLoad");
//[RuntimeInitializeOnLoadMethod] static void DefaultLog() => Debug.Log(Prefix + "RuntimeInit Default");
//void Start() => Debug.Log("Start");
//*/

namespace Vanilla.Init
{

	public interface IInitiable
	{

		void Init();
		void PostInit();

	}

	public static class Initializer
	{

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Reset()
		{
			#if UNITY_EDITOR
			SceneManager.sceneLoaded -= RunInit;
			#endif
			
			SceneManager.sceneLoaded += RunInit;
		}
		
		// For some reason, the first scene load doesn't invoke SceneManager.sceneLoader. Go figure.
		// illdoitmyself.gif
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void AfterSceneLoad()
		{
			// The Editor is unique in that it can start with multiple scenes open.
			// Builds don't have this condition.
			#if UNITY_EDITOR
			for (var i = 0;
			     i < SceneManager.sceneCount;
			     i++)
			{
				var scene = SceneManager.GetSceneAt(i);

				// If you open a scene in the Editor but then unload it, it still appears in the list of loaded scenes
				// So we have to manually check ourselves...
				if (scene.isLoaded) Init(scene);
			}
			#else
			Init(SceneManager.GetActiveScene());
			#endif
		}


		private static void RunInit(Scene scene,
		                            LoadSceneMode loadSceneMode) => Init(scene);

		public static void Init(Scene scene)
		{
			
			#if debug
			Debug.LogWarning($"[Initializer] Initialization begun");
			#endif

			var initiables = scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<IInitiable>(includeInactive: true));

			foreach (var i in initiables) i.Init();

			foreach (var i in initiables) i.PostInit();

			#if debug
			Debug.LogWarning($"[Initializer] Initialization complete");
			#endif
			
		}

	}

}
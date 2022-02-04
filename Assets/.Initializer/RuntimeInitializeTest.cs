//using UnityEngine;
//
//namespace Vanilla.Initializer
//{
//
//	class RuntimeInitializeTest
//	{
//
//		// These are listed in order of execution
//
//		// The following are only called once upon entering play mode:
//
//		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
//		static void SubsystemRegistration() => Debug.Log(RuntimeInitializeLoadType.SubsystemRegistration.ToString());
//
//
//		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
//		static void AfterAssemblies() => Debug.Log(RuntimeInitializeLoadType.AfterAssembliesLoaded.ToString());
//
//
//		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
//		static void BeforeSplashScreen() => Debug.Log(RuntimeInitializeLoadType.BeforeSplashScreen.ToString());
//
//
//		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
//		static void BeforeSceneLoad() => Debug.Log(RuntimeInitializeLoadType.BeforeSceneLoad.ToString());
//
//
//		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
//		static void AfterSceneLoad() => Debug.Log(RuntimeInitializeLoadType.AfterSceneLoad.ToString());
//
//
//		[RuntimeInitializeOnLoadMethod]
//		static void OnRuntimeMethodLoad() => Debug.Log("RuntimeInitializeOnLoadMethod 1");
//
//
//		[RuntimeInitializeOnLoadMethod]
//		static void OnSecondRuntimeMethodLoad() => Debug.Log("RuntimeInitializeOnLoadMethod 2");
//
//
//		[RuntimeInitializeOnLoadMethod]
//		static void OnThirdRuntimeMethodLoad() => Debug.Log("RuntimeInitializeOnLoadMethod 3");
//
//	}
//
//}
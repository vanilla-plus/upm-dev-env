#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Vanilla.AddressableSceneManager
{

	public static class AddressableSceneManager
	{

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Deinitialize()
		{
			#if debug
//			Debug.Log("ContentSceneManager.Deinitialize");
			#endif
		}
	
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void StaticReset()
		{
			#if debug
//			Debug.Log("ContentSceneManager.StaticReset");
			#endif

			instances = new Dictionary<string, SceneInstance>();
		}
	
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		private static void Initialize()
		{
			#if debug
//			Debug.Log("ContentSceneManager.Initialize");
			#endif
		}
	
		[NonSerialized]
		public static Dictionary<string, SceneInstance> instances = new();

		public static async UniTask<SceneInstance> TryLoadSceneInstance(AssetReference assRef, LoadSceneMode loadMode = LoadSceneMode.Additive, bool activateOnLoad = true)
		{
			var key = assRef.RuntimeKey.ToString();
		
			if (instances.ContainsKey(key))
			{
				#if UNITY_EDITOR
				Debug.LogError($"Instance already exists for {assRef.editorAsset.name}");
				#elif debug
			Debug.LogError($"Instance already exists for {key}");
				#endif
			
				return instances[key];
			}

			var instance = await assRef.LoadSceneAsync(loadMode,
			                                           activateOnLoad).ToUniTask();

			instances.Add(key, instance);
		
			#if UNITY_EDITOR
			Debug.Log($"Instance successfully loaded for {assRef.editorAsset.name}");
			#elif debug
		Debug.Log($"Instance successfully loaded for {key}");
			#endif

			return instance;
		}


		public static async Task TryUnloadSceneInstance(AssetReference assRef)
		{
			var key = assRef.RuntimeKey.ToString();

			if (!instances.ContainsKey(key))
			{
				#if UNITY_EDITOR
				Debug.LogError($"No instance exists for {assRef.editorAsset.name}");
				#elif debug
			Debug.LogError($"No instance exists for {key}");
				#endif

				return;
			}

			await Addressables.UnloadSceneAsync(instances[key],
			                                    UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);

			instances.Remove(key);
		
			#if UNITY_EDITOR
			Debug.Log($"Instance successfully unloaded for {assRef.editorAsset.name}");
			#elif debug
		Debug.Log($"Instance successfully unloaded for {key}");
			#endif
		}
	
	}

}
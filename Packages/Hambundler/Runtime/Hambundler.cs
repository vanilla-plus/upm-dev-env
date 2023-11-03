#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

namespace Vanilla.Hambundler
{
    public static class Hambundler
    {

        public static string RemoteBundlePathRoot;
        
        public static Dictionary<string, AssetBundle> Bundles = new Dictionary<string, AssetBundle>();


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Reset()
        {
            foreach (var v in Bundles.Values)
            {
                if (v != null)
                {
                    v.Unload(true);
                }
            }

            Bundles.Clear();
        }


        public static async UniTask LoadLocal(string path,
                                              string name,
                                              Action<float> OnProgress)
        {
            if (Bundles.ContainsKey(name))
            {
                Debug.Log($"The bundle [{name}] has already been loaded.");

                return;
            }

            #if debug
            Debug.Log($"AssetBundle local load begun - [{path}]");
            #endif

            var op = AssetBundle.LoadFromFileAsync(path);

            while (!op.isDone)
            {
                OnProgress?.Invoke(op.progress);

                await UniTask.Yield();
            }

            #if debug
            Debug.Log($"AssetBundle local load successful - [{path}]");
            #endif

            Bundles.Add(key: name,
                        value: op.assetBundle);
        }


        public static async UniTask LoadRemote(string url,
                                               string name,
                                               Action<float> OnProgress)
        {
            if (Bundles.ContainsKey(name))
            {
                #if debug
                Debug.Log($"The bundle [{name}] has already been loaded.");
                #endif

                return;
            }

            using var request = UnityWebRequestAssetBundle.GetAssetBundle(uri: Path.Combine(path1: RemoteBundlePathRoot,
                                                                                            path2: url));

            var op = request.SendWebRequest();

            while (!op.isDone)
            {
                OnProgress?.Invoke(op.progress);

                await UniTask.Yield();
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);

                throw new Exception(request.error);
            }

            #if debug
            Debug.Log($"AssetBundle remote load successful - [{name}] from [{url}]");
            #endif

            var bundle = DownloadHandlerAssetBundle.GetContent(request);

            Bundles.Add(key: name,
                        value: bundle);

            request.downloadHandler?.Dispose();
            request.Dispose();
        }

        public static async UniTask Unload(string name,
                                           Action<float> OnProgress)
        {
            Bundles.TryGetValue(key: name,
                                value: out var bundle);

            if (bundle == null)
            {
                Debug.LogError($"No loaded bundle by the name [{name}]");

                return;
            }

            Bundles.Remove(name);

            #if debug
            Debug.Log($"AssetBundle unload begun - [{name}]");
            #endif

            var op = bundle.UnloadAsync(true);

            while (!op.isDone)
            {
                OnProgress?.Invoke(op.progress);

                await UniTask.Yield();
            }

            #if debug
            Debug.Log($"AssetBundle unload successful - [{name}]");
            #endif
        }

    }
}
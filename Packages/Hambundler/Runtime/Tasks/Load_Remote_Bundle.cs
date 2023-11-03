using System;
using System.IO;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

using Vanilla.MetaScript;

namespace Vanilla.Hambundler
{
	
	[Serializable]
    public class Load_Remote_Bundle : MetaTask
    {
	    
	    [SerializeField]
        public string        bundleURL;
        [SerializeField]
        public string        bundleName;
        public Action<float> OnDownloadProgress;

        protected override bool CanAutoName() => !string.IsNullOrWhiteSpace(bundleURL);


        protected override string CreateAutoName() => $"Load bundle [{bundleName}] from remote path [{bundleURL}]";


        public override void OnValidate()
        {
	        #if UNITY_EDITOR
	        base.OnValidate();
	        
	        bundleName = Path.GetFileName(bundleURL);
	        #endif
        }


        protected override async UniTask<Scope> _Run(Scope scope)
        {
	        if (Hambundler.Bundles.ContainsKey(bundleName))
	        {
		        #if debug
		        Debug.Log($"The bundle [{bundleName}] has already been loaded.");
		        #endif

		        return scope;
	        }

	        using var request = UnityWebRequestAssetBundle.GetAssetBundle(uri: Path.Combine(path1: Hambundler.RemoteBundlePathRoot,
	                                                                                        path2: bundleURL));

	        var op = request.SendWebRequest();

	        while (!op.isDone)
	        {
		        if (scope.Cancelled) return scope;

		        OnDownloadProgress?.Invoke(op.progress);

		        await UniTask.Yield();
	        }

	        if (request.result != UnityWebRequest.Result.Success)
	        {
		        Debug.Log(request.error);

		        throw new Exception(request.error);
	        }

	        #if debug
	        Debug.Log($"AssetBundle remote load successful - [{bundleName}] from [{bundleURL}]");
	        #endif

	        var bundle = DownloadHandlerAssetBundle.GetContent(request);

	        Hambundler.Bundles.Add(key: bundleName,
	                               value: bundle);

	        request.downloadHandler?.Dispose();
	        request.Dispose();

	        return scope;
        }

    }
}

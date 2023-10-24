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

	        bundleName = Path.GetFileNameWithoutExtension(bundleURL);
	        #endif
        }


        protected override async UniTask<Scope> _Run(Scope scope)
        {
	        if (Hambundler.Bundles.ContainsKey(bundleName))
	        {
		        Debug.Log($"The bundle [{bundleName}] has already been loaded.");

		        return scope;
	        }

	        using var request = UnityWebRequestAssetBundle.GetAssetBundle(Path.Combine(Hambundler.RemoteBundlePathRoot,
	                                                                                   bundleURL));

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
        
	        // Write DownloadHandler bytes to local file for caching?
	        // ToDo - Never think about caching again
        
//	        #if debug
//            Debug.Log($"AssetBundle remote load successful - [{bundleName}] from [{url}]");
//	        #endif
            
	        var bundle = DownloadHandlerAssetBundle.GetContent(request);

	        Hambundler.Bundles.Add(bundleName,
	                               bundle);

	        request.downloadHandler?.Dispose();
	        request.Dispose();

	        return scope;
        }

    }
}

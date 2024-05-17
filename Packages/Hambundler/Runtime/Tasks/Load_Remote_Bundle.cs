using System;
using System.IO;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

using Vanilla.MetaScript;
using Vanilla.MetaScript.DataSources.Strings;
using Vanilla.TypeMenu;

namespace Vanilla.Hambundler
{
	
	[Serializable]
    public class Load_Remote_Bundle : MetaTask
    {
	 
	    [TypeMenu("red")]
	    [SerializeReference] public StringSource BundleURL;

	    [TypeMenu("red")]
	    [SerializeReference] public StringSource BundleName;
	    
//	    [SerializeField]
//        public string        bundleURL;
//        [SerializeField]
//        public string        bundleName;
        public Action<float> OnDownloadProgress;

        protected override bool Validate => BundleURL != null && !string.IsNullOrWhiteSpace(BundleURL.Value);


        protected override string CreateAutoName() => $"Load bundle [{BundleName.Value}] from remote path [{BundleURL.Value}]";


        public override void OnValidate()
        {
	        #if UNITY_EDITOR
	        base.OnValidate();
	        
	        BundleName.Value = Path.GetFileName(BundleURL.Value);
	        #endif
        }


        protected override async UniTask<Scope> _Run(Scope scope)
        {
	        if (Hambundler.Bundles.ContainsKey(BundleName.Value))
	        {
		        #if debug
		        Debug.Log($"The bundle [{BundleName.Value}] has already been loaded.");
		        #endif

		        return scope;
	        }

	        using var request = UnityWebRequestAssetBundle.GetAssetBundle(uri: BundleURL.Value);

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
	        Debug.Log($"AssetBundle remote load successful - [{BundleName.Value}] from [{BundleURL.Value}]");
	        #endif

	        var bundle = DownloadHandlerAssetBundle.GetContent(request);

	        Hambundler.Bundles.Add(key: BundleName.Value,
	                               value: bundle);

	        request.downloadHandler?.Dispose();
	        request.Dispose();

	        return scope;
        }

    }
}

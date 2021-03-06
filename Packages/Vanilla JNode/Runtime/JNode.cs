#if DEBUG && JNODE
#define debug
#endif

using System;
using System.IO;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

#if RemoteConfigInstalled
using Unity.RemoteConfig;
#endif

using UnityEngine;
using UnityEngine.Networking;

namespace Vanilla.JNode
{

	[Serializable]
	public abstract class JNode
	{

		[NonSerialized]
		protected internal bool _initialized = false;
		
		[NonSerialized]
		protected JToken _token;
		public JToken Token => _token;

		public string ToJson(bool prettyPrint = false) => JsonUtility.ToJson(obj: this,
		                                                                     prettyPrint: prettyPrint);

		internal abstract void OnValidate();

		public abstract bool AutoUpdateJToken();

//		internal abstract UniTask Initialize<C, N>(C collection) where C : JnodeCollection<N>
//		                                                         where N : Jnode;

//		internal abstract UniTask Initialize<I>(JnodeCollection<I> collection) where I : Jnode;
		
		internal abstract UniTask Initialize();

		internal abstract UniTask Refresh();

		internal abstract UniTask Deinitialize();


		#if RemoteConfigInstalled
		public struct userAttributes { }
		public struct appAttributes { }

		public async UniTask FromRemoteConfig(string rootKey,
		                                      string fallback)
		{
			ConfigManager.FetchConfigs(userAttributes: new userAttributes(),
			                           appAttributes: new appAttributes());

			while (ConfigManager.requestStatus == ConfigRequestStatus.Pending) await UniTask.Yield();

			if (ConfigManager.requestStatus == ConfigRequestStatus.Failed)
			{
				#if debug
				Debug.LogError(message: "Fetch from remote config failed.");
				#endif

				return;
			}

			var json = ConfigManager.appConfig.GetJson(key: rootKey,
			                                           defaultValue: fallback);

			await FromJson(json: json);

		}


		public async UniTask FromRemoteConfig<U, A>(string rootKey,
		                                            string fallback,
		                                            U userAttributes,
		                                            A appAttributes)
			where U : struct
			where A : struct
		{
			ConfigManager.FetchConfigs(userAttributes: userAttributes,
			                           appAttributes: appAttributes);

			while (ConfigManager.requestStatus == ConfigRequestStatus.Pending) await UniTask.Yield();

			if (ConfigManager.requestStatus == ConfigRequestStatus.Failed)
			{
				#if debug
				Debug.LogError(message: "Fetch from remote config failed.");
				#endif

				return;
			}

			var json = ConfigManager.appConfig.GetJson(key: rootKey,
			                                           defaultValue: fallback);

			await FromJson(json: json);

		}
		#endif
		
		public async UniTask FromWebRequest(string url)
		{
			var request = new UnityWebRequest(url: url,
			                                  method: UnityWebRequest.kHttpVerbGET,
			                                  downloadHandler: new DownloadHandlerBuffer(),
			                                  uploadHandler: null);

			await request.SendWebRequest();

			if (!string.IsNullOrEmpty(request.error))
			{
				#if debug
				Debug.LogError(message: request.error);
				#endif

				return;
			}

			await FromJson(json: request.downloadHandler.text);
		}


		public async UniTask FromLocalFile(string path)
		{
			var json = await File.ReadAllTextAsync(path: path);

			await FromJson(json: json);
		}

		public virtual async UniTask FromJson(string json)
		{
			JsonUtility.FromJsonOverwrite(json: json,
			                              objectToOverwrite: this);

			if (AutoUpdateJToken()) UpdateJToken();

			if (!_initialized)
			{
				_initialized = true;

				await Initialize();
			}
			else
			{
				await Refresh();
			}
		}


		public void UpdateJToken() => _token = JToken.Parse(ToJson());


	}

}
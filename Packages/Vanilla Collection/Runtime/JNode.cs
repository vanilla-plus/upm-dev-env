#if DEBUG && JNODE
#define debug
#endif

using System;
using System.IO;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Unity.RemoteConfig;

using UnityEngine;
using UnityEngine.Networking;

namespace Vanilla.JNode
{

	[Serializable]
	public abstract class JNode
	{

		[NonSerialized]
		protected JToken _data;
		public JToken Data => _data;

		public string ToJson(bool prettyPrint = false) => JsonUtility.ToJson(obj: this,
		                                                                     prettyPrint: prettyPrint);


		internal abstract void OnValidate();

		public abstract bool AutoUpdateJToken();

		internal abstract UniTask AddedToCollection();

		internal abstract UniTask RemovedFromCollection();


		#if RemoteConfigInstalled
		public struct userAttributes { }
		public struct appAttributes { }


		public async static UniTask FetchRemCon()
		{
			ConfigManager.FetchConfigs(userAttributes: new userAttributes(),
			                           appAttributes: new appAttributes());

			while (ConfigManager.requestStatus == ConfigRequestStatus.Pending) await UniTask.Yield();

			#if debug
			if (ConfigManager.requestStatus == ConfigRequestStatus.Failed)
			{
				Debug.LogError(message: $"Remote config fetch [{ConfigManager.requestStatus}]");
			}
			else
			{
				Debug.Log(message: $"Remote config fetch [{ConfigManager.requestStatus}]");
			}
			#endif
		}

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
				Debug.LogError(message: "Fetch from remote config failed.");

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


		public virtual UniTask FromJson(string json)
		{
			JsonUtility.FromJsonOverwrite(json: json,
			                              objectToOverwrite: this);
			
			if (AutoUpdateJToken()) UpdateJToken();

			return default;
		}
		
		public void UpdateJToken() => _data = JToken.Parse(ToJson());


	}

}
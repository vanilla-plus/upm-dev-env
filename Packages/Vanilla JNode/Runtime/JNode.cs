#if DEBUG && JNODE
#define debug
#endif

using System;
using System.IO;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

#if RemoteConfigInstalled
using Unity.Services.RemoteConfig;
#endif

using UnityEngine;
using UnityEngine.Networking;

using static Unity.Services.RemoteConfig.ConfigRequestStatus;

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


		public abstract void OnValidate();

		[Tooltip("JTokens allow us to access weakly-typed properties and fields, JavaScript style.")]
		public abstract bool AutoUpdateJToken();

//		internal abstract UniTask Initialize<C, N>(C collection) where C : JnodeCollection<N>
//		                                                         where N : Jnode;

//		internal abstract UniTask Initialize<I>(JnodeCollection<I> collection) where I : Jnode;

		public abstract UniTask Initialize();

		public abstract UniTask Refresh();

		public abstract UniTask Deinitialize();


		#if RemoteConfigInstalled
		public struct userAttributes { }
		public struct appAttributes { }


		public async UniTask<bool> FetchRemoteConfig()
		{
			RemoteConfigService.Instance.FetchConfigs(userAttributes: new userAttributes(),
			                                          appAttributes: new appAttributes());
			
//			ConfigManager.FetchConfigs(userAttributes: new userAttributes(),
//			                           appAttributes: new appAttributes());

			while (RemoteConfigService.Instance.requestStatus == Pending) await UniTask.Yield();

			#if debug
			Debug.LogError(message: $"Remote config fetch result [{ConfigManager.requestStatus}].");
			#endif
			
			return RemoteConfigService.Instance.requestStatus != Failed;
		}
		
		public async UniTask<bool> FetchRemoteConfig<U,A>(U userAttributes,
		                                                  A appAttributes)
			where U : struct
			where A : struct
		{
			RemoteConfigService.Instance.FetchConfigs(userAttributes: userAttributes,
			                                          appAttributes: appAttributes);

			while (RemoteConfigService.Instance.requestStatus == Failed) await UniTask.Yield();

			#if debug
			Debug.LogError(message: $"Remote config fetch result [{ConfigManager.requestStatus}].");
			#endif
			
			return RemoteConfigService.Instance.requestStatus != Failed;
		}

		public async UniTask FromRemoteConfig(string rootKey,
		                                      string fallback)
		{
			if (!await FetchRemoteConfig()) return;

//			var json = ConfigManager.appConfig.GetJson(key: rootKey,
//			                                           defaultValue: fallback);

			var json = RemoteConfigService.Instance.appConfig.GetJson(key: rootKey,
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
			RemoteConfigService.Instance.FetchConfigs(userAttributes: userAttributes,
			                                          appAttributes: appAttributes);

			if (!await FetchRemoteConfig(userAttributes: userAttributes,
			                             appAttributes: appAttributes)) return;

			var json = RemoteConfigService.Instance.appConfig.GetJson(key: rootKey,
			                                                          defaultValue: fallback);

			await FromJson(json: json);

		}
		#endif
		
		public async UniTask FromWebRequest(string url)
		{
			var request = new UnityWebRequest(url: url,
			                                  method: UnityWebRequest.kHttpVerbGET,
			                                  downloadHandler: new DownloadHandlerBuffer(), // ToDo - could this be cached and re-used instead of new'd?
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
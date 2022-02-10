using System;
using System.IO;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Unity.RemoteConfig;

using UnityEngine;
using UnityEngine.Networking;

namespace Vanilla.MenuMachine
{

	public static class MenuMachine
	{

		public struct userAttributes { }

		public struct appAttributes { }

		public static JObject RawData;

		public static Action OnCatalogueFetchBegun;
		public static Action OnCatalogueFetchFailed;
		public static Action OnCatalogueFetchSuccess;
		public static Action OnFirstCatalogueFetchSuccess;


		public static async UniTask FetchViaRemoteConfig<C, I>(C catalogue)
			where C : class, ICatalogue<I>
			where I : class, ICatalogueItem
		{
			try
			{
				OnCatalogueFetchBegun?.Invoke();

				ConfigManager.FetchConfigs(userAttributes: new userAttributes(),
				                           appAttributes: new appAttributes());

				while (ConfigManager.requestStatus == ConfigRequestStatus.Pending) await UniTask.Yield();

				if (ConfigManager.requestStatus == ConfigRequestStatus.Failed)
				{
					OnCatalogueFetchFailed?.Invoke();

					return;
				}

				RawData = ConfigManager.appConfig.config.First as JObject;

				var json = ConfigManager.appConfig.GetJson(key: "manifest",
				                                           defaultValue: catalogue.DefaultRemoteConfig);

				await FetchViaJson<C, I>(catalogue: catalogue,
				                         json: json);
			}
			catch (Exception e)
			{
				OnCatalogueFetchFailed?.Invoke();

				Debug.LogException(exception: e);
			}
		}


		public static async UniTask FetchViaWebRequest<C, I>(C catalogue,
		                                                     string url)
			where C : class, ICatalogue<I>
			where I : class, ICatalogueItem
		{
			try
			{
				OnCatalogueFetchBegun?.Invoke();

				var request = new UnityWebRequest(url: url,
				                                  method: UnityWebRequest.kHttpVerbGET,
				                                  downloadHandler: new DownloadHandlerBuffer(),
				                                  uploadHandler: null);

				await request.SendWebRequest();

				if (!string.IsNullOrEmpty(request.error))
				{
					Debug.Log(message: request.error);

					OnCatalogueFetchFailed?.Invoke();

					return;
				}

				await FetchViaJson<C, I>(catalogue: catalogue,
				                         json: request.downloadHandler.text);
			}
			catch (Exception e)
			{
				OnCatalogueFetchFailed?.Invoke();

				Debug.LogException(exception: e);
			}
		}


		public static async UniTask FetchViaLocalFile<C, I>(C catalogue,
		                                                    string path)
			where C : class, ICatalogue<I>
			where I : class, ICatalogueItem
		{
			try
			{
				var json = await File.ReadAllTextAsync(path: path);

				await FetchViaJson<C, I>(catalogue: catalogue,
				                         json: json);
			}
			catch (Exception e)
			{
				OnCatalogueFetchFailed?.Invoke();

				Debug.LogException(exception: e);
			}
		}


		public static async UniTask FetchViaJson<C, I>(C catalogue,
		                                               string json)
			where C : class, ICatalogue<I>
			where I : class, ICatalogueItem
		{
			try
			{

				RawData ??= JObject.Parse(json: json);

				// Bail early if something went wrong with parsing.
				
				if (!RawData.HasValues)
				{
					OnCatalogueFetchFailed?.Invoke();

					return;
				}

//				Debug.Log(RawData);

				var firstCatalogueFetch = !catalogue.Initialized;

				if (firstCatalogueFetch)
				{
					catalogue.Initialized = true;

					await catalogue.Initialize();
				}

				JsonUtility.FromJsonOverwrite(json: json,
				                              objectToOverwrite: catalogue);

				await UniTask.Yield();

				await catalogue.Update();

				var i = -1;

				foreach (var newItem in catalogue.Items)
				{
					newItem.RawData = RawData[propertyName: "_items"]?[key: ++i];

					await newItem.Initialize();
				}

				if (firstCatalogueFetch) OnFirstCatalogueFetchSuccess?.Invoke();

				OnCatalogueFetchSuccess?.Invoke();
			}
			catch (Exception e)
			{
				OnCatalogueFetchFailed?.Invoke();

				Debug.LogException(exception: e);
			}
		}

	}

}
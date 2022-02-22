using System;
using System.IO;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Unity.RemoteConfig;

using UnityEngine;
using UnityEngine.Networking;

namespace Vanilla.Catalogue
{

	public static class CatalogueBuilder
	{

		public struct userAttributes { }

		public struct appAttributes { }

		private static bool _initialized;

		public static JObject RawData;

		private static int _CurrentCatalogueVersion = -1;
		public static  int CurrentCatalogueVersion => _CurrentCatalogueVersion;

		public static Action OnCatalogueFetchBegun;
		public static Action OnCatalogueFetchFailed;
		public static Action OnCatalogueUpToDate;
		public static Action OnCatalogueFetchSuccess;

		public static Action OnInitialize;

		#if RemoteConfigInstalled
		public static async UniTask FetchViaRemoteConfig<C, I>(C catalogue,
		                                                       string fallback)
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
				                                           defaultValue: fallback);

				await FetchViaJson<C, I>(catalogue: catalogue,
				                         json: json);
			}
			catch (Exception e)
			{
				OnCatalogueFetchFailed?.Invoke();

				Debug.LogException(exception: e);
			}
		}
		#endif


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


		public static async UniTask FetchViaEditor<C, I>(CatalogueEditor<C, I> editor)
			where C : class, ICatalogue<I>
			where I : class, ICatalogueItem => await FetchViaJson<C, I>(catalogue: editor.catalogue,
			                                                            json: editor.output);


		public static async UniTask FetchViaJson<C, I>(C catalogue,
		                                               string json)
			where C : class, ICatalogue<I>
			where I : class, ICatalogueItem
		{
			try
			{

				// This was preventing RawData from being populated a second time.
				
//				RawData ??= JObject.Parse(json: json);

				var incomingData = JObject.Parse(json: json);
				
				if (!incomingData.HasValues)
				{
					OnCatalogueFetchFailed?.Invoke();

					return;
				}

				var incomingCatalogueVersion = incomingData.Value<int>("_version");

				if (incomingCatalogueVersion <= _CurrentCatalogueVersion)
				{
					Debug.Log($"No catalogue updates available. [{_CurrentCatalogueVersion}] => [{incomingCatalogueVersion}]");

					OnCatalogueUpToDate?.Invoke();

					return;
				}

				Debug.Log($"Catalogue update available! [{_CurrentCatalogueVersion}] => [{incomingCatalogueVersion}]");
				
				_CurrentCatalogueVersion = incomingCatalogueVersion;

				RawData = JObject.Parse(json: json);

//				RawData                  = incomingData;

				await catalogue.PreFetch();

				JsonUtility.FromJsonOverwrite(json: json,
				                              objectToOverwrite: catalogue);
				
				var i = -1;
				
				var items                    = RawData[propertyName: "_items"];
//				var items = RawData.Value<I[]>("_items");

				if (items == null)
				{
					Debug.LogError("Catalogue contains no items array.");

					OnCatalogueFetchFailed?.Invoke();

					return;
				}

				foreach (var newItem in catalogue.Items)
				{
					newItem.RawData = items[key: ++i];

					await newItem.Initialize();
				}

				await catalogue.PostFetch();

				OnCatalogueFetchSuccess?.Invoke();

				if (!_initialized)
				{
					_initialized = true;

					await catalogue.Initialize();

					OnInitialize?.Invoke();
				}
			}
			catch (Exception e)
			{
				OnCatalogueFetchFailed?.Invoke();

				Debug.LogException(exception: e);
			}
		}

	}

}
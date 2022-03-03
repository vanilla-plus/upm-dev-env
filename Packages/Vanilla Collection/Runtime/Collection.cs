using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

#if RemoteConfigInstalled
using Unity.RemoteConfig;
#endif

using UnityEngine;
using UnityEngine.Networking;

namespace Vanilla.Collection
{

	[Serializable]
	public abstract class Collection<I>
		where I : CollectionItem
	{
		
		public struct userAttributes { }

		public struct appAttributes { }
		
		[NonSerialized]
		public Action FetchBegun;
		[NonSerialized]
		public Action FetchFailed;
		[NonSerialized]
		public Action FetchComplete;

		[NonSerialized]
		public Action<I> OnItemAdded;
		
		[NonSerialized]
		public Action<I> OnItemRemoved;
		
		[NonSerialized]
		private JObject _rawData;
		public  JObject RawData => _rawData;

		[Tooltip(tooltip: "Has this Collection been populated at least once?")]
		[NonSerialized]
		private bool _initialized;
		public bool Initialized => _initialized;
		
		[SerializeField]
		private HashSet<I> _items;
		public HashSet<I> Items => _items;

		protected abstract string ItemArrayKey
		{
			get;
		}
		
		#if RemoteConfigInstalled
		public async UniTask FetchViaRemoteConfig(string rootKey,
		                                          string fallback)
		{
			try
			{
				FetchBegun?.Invoke();

				ConfigManager.FetchConfigs(userAttributes: new userAttributes(),
				                           appAttributes: new appAttributes());

				while (ConfigManager.requestStatus == ConfigRequestStatus.Pending) await UniTask.Yield();

				if (ConfigManager.requestStatus == ConfigRequestStatus.Failed)
				{
					FetchFailed?.Invoke();
					
					return;
				}

				_rawData = ConfigManager.appConfig.config.First as JObject;

				_rawData = ConfigManager.appConfig.config.First as JObject;

				var json = ConfigManager.appConfig.GetJson(key: rootKey,
				                                           defaultValue: fallback);

				await FetchViaJson(json: json);
			}
			catch (Exception e)
			{
				FetchFailed?.Invoke();

				Debug.LogException(exception: e);
			}
		}
		#endif


		public async UniTask FetchViaWebRequest(string url)
		{
			try
			{
				FetchBegun?.Invoke();

				var request = new UnityWebRequest(url: url,
				                                  method: UnityWebRequest.kHttpVerbGET,
				                                  downloadHandler: new DownloadHandlerBuffer(),
				                                  uploadHandler: null);

				await request.SendWebRequest();

				if (!string.IsNullOrEmpty(request.error))
				{
					Debug.Log(message: request.error);

					FetchFailed?.Invoke();

					return;
				}

				await FetchViaJson(json: request.downloadHandler.text);
			}
			catch (Exception e)
			{
				FetchFailed?.Invoke();

				Debug.LogException(exception: e);
			}
		}


		public async UniTask FetchViaLocalFile(string path)
		{
			try
			{
				var json = await File.ReadAllTextAsync(path: path);

				await FetchViaJson(json: json);
			}
			catch (Exception e)
			{
				FetchFailed?.Invoke();

				Debug.LogException(exception: e);
			}
		}


//		public static async UniTask FetchViaEditor<C, I>(CatalogueEditor<C, I> editor,
//		                                                 string itemArrayKey)
//			where C : class, ICatalogue<I>
//			where I : class, ICatalogueItem => await FetchViaJson<C, I>(catalogue: editor.catalogue,
//			                                                            itemArrayKey: itemArrayKey,
//			                                                            json: editor.output);


		public async UniTask FetchViaJson(string json)
		{
			try
			{
				_initialized = _items is
				               {
					               Count: > 0
				               };

				// Does _items get new'd during FromJson?
				// Or does the same collection just get overridden?
				
				var _itemsCache = _initialized ?
					                  new HashSet<I>(collection: _items) :
					                  null;
				
				_rawData = JObject.Parse(json: json);

				if (!_rawData.HasValues)
				{
					FetchFailed?.Invoke();

					return;
				}
				
				JsonUtility.FromJsonOverwrite(json: json,
				                              objectToOverwrite: this);

				if (_initialized)
				{
					await HandleRemovedItems(removedItems: _itemsCache.Except(second: _items));

					await HandleNewItems(newItems: _items.Except(second: _itemsCache));
				}
				else
				{
					await HandleNewItems(newItems: _items);
				}

//				var i = -1;

//				if (items == null)
//				{
//					Debug.LogError("Catalogue contains no items array.");
//
//					FetchFailed?.Invoke();
//
//					return;
//				}

//				foreach (var newItem in _items) await newItem.Initialize(data: items[key: ++i]);

//				await PostFetch();

				FetchComplete?.Invoke();

//				if (!_initialized)
//				{
//					_initialized = true;

//					await Initialize();

//					OnInitialize?.Invoke();
//				}

//				_initialized = true;
			}
			catch (Exception e)
			{
				FetchFailed?.Invoke();

				Debug.LogException(exception: e);
			}
		}


		protected virtual async UniTask HandleRemovedItems(IEnumerable<I> removedItems)
		{
			foreach (var i in removedItems)
			{
				await i.Removed();
				
				OnItemRemoved?.Invoke(i);
			}
		}
		
		protected virtual async UniTask HandleNewItems(IEnumerable<I> newItems)
		{
			foreach (var i in newItems)
			{
				await i.Added();
				
				OnItemAdded?.Invoke(i);
			}
		}

	}

}
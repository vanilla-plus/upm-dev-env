using System;
using System.Linq;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

using UnityEngine;

namespace Vanilla.JNode
{

	[Serializable]
	public abstract class JNodeCollection<I> : JNode
		where I : JNode
	{

		protected const string c_DefaultCollectionName = "_items";

		[SerializeField]
		protected I[] _items = Array.Empty<I>();
		public   I[] Items => _items;

		public abstract UniTask ItemAdded(I item);
		public abstract UniTask ItemRemoved(I item);

		public Action<I> OnItemAdded;

		public Action<I> OnItemRemoved;


		public override async UniTask FromJson(string json)
		{
			// Let's check if Items isn't null and has more than 0 items.
			// If it does, we should do extra work to cache and compare with
			// any items that are no longer present.
			// This will allow implementations to simply worry about 'new' or 'removed' items!
			
			var someItemsAlreadyPresent = Items is
			                              {
				                              Length: > 0
			                              };

			var itemsCache = someItemsAlreadyPresent ?
				                 Items.Clone() as I[] :
				                 Array.Empty<I>();

			JsonUtility.FromJsonOverwrite(json: json,
			                              objectToOverwrite: this);
			
			if (AutoUpdateJToken()) UpdateJToken();

			if (someItemsAlreadyPresent)
			{
				foreach (var @old in itemsCache.Except(Items))
				{
					await HandleRemovedItem(removedItem: @old);
				}
				
				foreach (var @new in _items.Except(itemsCache))
				{
					await HandleNewItem(newItem: @new);
				}
			}
			else
			{
				foreach (var @new in Items)
				{
					await HandleNewItem(newItem: @new);
				}
			}
		}


//		public async UniTask FromJsonButItsJustTheItemArrayPlz(string json) => _items = JsonUtility.FromJson<I[]>(json: json); // Doesn't work..?
//		public async UniTask FromJsonButItsJustTheItemArrayPlz(string json) => _items = (I[]) JsonUtility.FromJson(json: json,
//		                                                                                                           type: typeof(I[])); // Also doesn't work


		protected virtual async UniTask HandleNewItem(I newItem)
		{
			if (newItem.AutoUpdateJToken()) newItem.UpdateJToken();

			await newItem.AddedToCollection();

			await ItemAdded(item: newItem);

			OnItemAdded?.Invoke(obj: newItem);
		}


		protected virtual async UniTask HandleRemovedItem(I removedItem)
		{
			await removedItem.RemovedFromCollection();

			await ItemRemoved(item: removedItem);

			OnItemRemoved?.Invoke(obj: removedItem);
		}


		internal override async UniTask RemovedFromCollection()
		{
			if (_items != null)
			{
				foreach (var i in _items) await HandleRemovedItem(removedItem: i);
			}
		}


		protected virtual string GetCollectionName() => c_DefaultCollectionName;

	}

}
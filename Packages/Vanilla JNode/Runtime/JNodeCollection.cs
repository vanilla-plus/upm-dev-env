using System;
using System.Linq;

using Cysharp.Threading.Tasks;

using UnityEngine;

//using Vanilla.JNode.Samples;

namespace Vanilla.JNode
{

	[Serializable]
	public abstract class JNodeCollection<I> : JNode
		where I : JNode, IEquatable<I>
	{

		// We only enforce a getter for the item collection here so that the real name of
		// the collection can difference depending on the context.
		// If we included the real array here, it would have to take a specific name like "_items"
		// which would be a strict convention for JSON layouts.
		public abstract I[] Items
		{
			get;
		}

		protected abstract UniTask ItemAdded(I item);
		protected abstract UniTask ItemRemoved(I item);

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

//			if (this is Earth)
//			{
//				Debug.Log(Items.Length);
//				Debug.Log(itemsCache.Length);
//
//				Debug.Log(ReferenceEquals(Items,
//				                          itemsCache));
//			}

			JsonUtility.FromJsonOverwrite(json: json,
			                              objectToOverwrite: this);

//			if (this is Earth)
//			{
//				Debug.Log(Items.Length);
//				Debug.Log(itemsCache.Length);
//
//				Debug.Log(ReferenceEquals(Items,
//				                          itemsCache));
//			}

			if (AutoUpdateJToken()) UpdateJToken();
			
			if (someItemsAlreadyPresent)
			{
//				foreach (var @old in itemsCache.Except(Items))
//				{
//					await ItemRemovedInternal(removedItem: @old);
//				}
//				
//				foreach (var @new in Items.Except(itemsCache))
//				{
//					await ItemAddedInternal(newItem: @new);
//				}

//				Debug.Log(Items[0].Token);
//				Debug.Log(itemsCache[0].Token);

//				Debug.Log(ReferenceEquals(Items[0],
//				                          itemsCache[0]));

				foreach (var o in itemsCache)
				{
					if (Items.Contains(o)) continue;
					
					await ItemRemovedInternal(removedItem: o);
				}
				
				foreach (var n in Items)
				{
					if (itemsCache.Contains(n)) continue;
					
					await ItemAddedInternal(newItem: n);
				}


//				if (this is Earth)
//				{
//					foreach (var @old in itemsCache)
//					{
//						Debug.LogWarning($"Regarding @old [{@old}]...");
//						
//						Debug.Log("Array.Contains?");
//						Debug.LogError(Items.Contains(@old));
//						
//						foreach (var @new in Items)
//						{
//							Debug.Log($"Does {@old} .Equals {@new}?");
//
//							Debug.LogError(@old.Equals(@new));
//
//							Debug.Log("==");
//							Debug.LogError(@old == @new);
//						}
//					}
//				}

//				var i = -1;

//				++i;

//				foreach (var @old in itemsCache)
//				{
//					if (!Items.Contains(@old))
//					{
////						if (this is Earth)
////						{
////							try
////							{
////								Debug.Log($"Is the cached version [{itemsCache[i]}] the same object reference as the new version? [{Items[i]}]");
////							}
////							catch (Exception e)
////							{
////								Debug.LogException(e);
////							}
//
////							Debug.LogError(itemsCache[i].Equals(Items[i]));
//
////							Debug.LogWarning($"DEBUG Removing [{@old.Token}]");
////						}
//
//						await ItemRemovedInternal(removedItem: @old);
//					}
//				}

//				i = -1;

//				foreach (var @new in Items)
//				{
////					++i;
//
//					if (!itemsCache.Contains(@new))
//					{
//						if (this is Earth)
//						{
////							try
////							{
////								Debug.Log($"Is the cached version [{itemsCache[i]}] the same object reference as the new version? [{Items[i]}]");
////							}
////							catch (Exception e)
////							{
////								Debug.LogException(e);
////							}
//
////							Debug.LogError(ReferenceEquals(itemsCache[i],
////							                               Items[i]));
//							
////							Debug.LogWarning($"DEBUG Adding [{@new.Token}]");
//						}
//
//						await ItemAddedInternal(newItem: @new);
//					}
//				}
			}
			else
			{
				foreach (var @new in Items)
				{
					await ItemAddedInternal(newItem: @new);
				}
			}
			
			if (!_initialized)
			{
				_initialized = true;

				await Initialize();
			}
		}

		protected virtual async UniTask ItemAddedInternal(I newItem)
		{
			if (newItem.AutoUpdateJToken()) newItem.UpdateJToken();

			if (!newItem._initialized)
			{
				newItem._initialized = true;
				
				await newItem.Initialize();
			}
			else
			{
				await newItem.Refresh();
			}

			await ItemAdded(item: newItem);

			OnItemAdded?.Invoke(obj: newItem);
		}
		
		protected virtual async UniTask ItemRemovedInternal(I removedItem)
		{
			OnItemRemoved?.Invoke(obj: removedItem);

			if (removedItem._initialized)
			{
				removedItem._initialized = false;
				
				await removedItem.Deinitialize();
			}
			else
			{
				#if debug
				Debug.LogError(message: "A Jnode has been de-initialized twice in a row. This shouldn't be possible - how did you let this happen!?");
				#endif
			}

			await ItemRemoved(item: removedItem);
		}


		internal override async UniTask Deinitialize()
		{
			if (Items != null)
			{
				foreach (var i in Items) await ItemRemovedInternal(removedItem: i);
			}
		}

	}

}
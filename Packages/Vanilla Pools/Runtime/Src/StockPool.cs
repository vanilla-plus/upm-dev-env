#if DEBUG_VANILLA_POOLS
#define debug
#endif

using System;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

using static UnityEngine.Debug;

using Object = UnityEngine.Object;

namespace Vanilla.Pools
{

	[Serializable]
//	public class StockPool<P, PI> : IPool<P,PI>
//		where P : class, IPool<P,PI>
//		where PI : MonoBehaviour, IPoolItem<P,PI>
	public class StockPool : IPool
	{
		
		[SerializeField] protected Transform _inactiveParent;
		public Transform InactiveParent
		{
			get => _inactiveParent;
			set => _inactiveParent = value;
		}

		[SerializeField] private Transform _activeParent;
		public Transform ActiveParent
		{
			get => _activeParent;
			set => _activeParent = value;
		}

		[SerializeField] protected GameObject _prefab;
		public                     GameObject Prefab => _prefab;

		[SerializeField] private int _total = 20;
		public int Total
		{
			get => _total;
			set => _total = value;
		}

		[SerializeField] protected Stack<IPoolItem> _inactive = new();
		public                     Stack<IPoolItem> Inactive => _inactive ??= new Stack<IPoolItem>(capacity: Total);
		
		[SerializeField] protected HashSet<IPoolItem> _active = new();
		public                     HashSet<IPoolItem> Active => _active ??= new HashSet<IPoolItem>(capacity: Total);


		public void CreateAll()
		{
			while (Active.Count + Inactive.Count < Total)
			{
				var newItem = CreateItem();

				if (ReferenceEquals(objA: newItem,
				                    objB: null)) return;

//				newItem.gameObject.name = $"{typeof(PI).Name} [{Inactive.Count}]";

				newItem.Pool = this;

				Inactive.Push(item: newItem);
			}
		}

		public void DestroyAll()
		{
			foreach (var item in Active)
			{
				DestroyItem(item);
			}

			while (Inactive.TryPop(out var item))
			{
				DestroyItem(item);
			}

			Active.Clear();
			Inactive.Clear();
		}


		public IPoolItem CreateItem()
		{
//			#if UNITY_EDITOR
//			var output = PrefabUtility.InstantiatePrefab(assetComponentOrGameObject: Prefab,
//			                                             parent: InactiveParent) as GameObject;
//			#else
			var output = Object.Instantiate(original: _prefab,
			                                            parent: InactiveParent);
//			#endif

			var result = output.GetComponentInChildren<IPoolItem>(includeInactive: true);

			if (ReferenceEquals(result,
			                    null))
			{
				#if debug
//				LogWarning($"New instance of {typeof(PI).Name} didn't have a Component of that type anywhere in its hierarchy.");
				#endif
			}
			else
			{
				result.Pool = this;
			}

			return result;
		}

		public void DestroyItem(IPoolItem item) => Object.Destroy(item.gameObject);

		public IPoolItem Get()
		{
			if (!Inactive.TryPop(out var item))
			{
				#if debug
//				LogWarning($"[{typeof(PI).Name}] pool exhausted!");
				#endif
				
				return null;
			}

//			if (item.Leased)
//			{
//				#if debug
//				LogWarning($"[{item}] was not returned to its [{typeof(PI).Name}] pool correctly.");
//				#endif
//			}

//			item.Leased = true;

			Active.Add(item: item);

			item.transform.SetParent(p: _activeParent);

			item.gameObject.SetActive(true);

//			item.HandleGet();

			return item;
		}

		public void Retire(IPoolItem item)
		{
//			if (!item.Leased) return;

//			item.Leased = false;
			
			Active.Remove(item);

			Inactive.Push(item);

			item.transform.SetParent(p: _inactiveParent);
			
			item.gameObject.SetActive(false);

//			item.HandleRetire();
		}


		public void RetireAll()
		{
			foreach (var item in Active.ToArray())
			{
				Retire(item);
			}
		}

	}

}
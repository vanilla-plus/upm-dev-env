#if DEBUG_VANILLA_POOLS
#define debug
#endif

using System;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Cysharp.Threading.Tasks;

using UnityEngine;

using static UnityEngine.Debug;

using Object = UnityEngine.Object;

namespace Vanilla.Pools
{

	[Serializable]
//	public class RingPool<P, PI> : IPool<P,PI>
//		where P : class, IPool<P,PI>
//		where PI : MonoBehaviour, IPoolItem<P,PI>
	public class RingPool : IPool
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
		public int       Total
		{
			get => _total;
			set => _total = value;
		}

		[NonSerialized] private int _index = -1;
		public                   int Index => _index;

		[NonSerialized] private IPoolItem[] _items;
		public                  IPoolItem[] Items => _items ??= new IPoolItem[Total];
		
		public void CreateAll()
		{
			for (var i = 0;
			     i < Items.Length;
			     i++)
			{
				Items[i] ??= CreateItem();
			}
		}

		public void DestroyAll()
		{
			for (var i = 0;
			     i < Items.Length;
			     i++)
			{
				DestroyItem(Items[i]);
			}
		}

		public IPoolItem CreateItem()
		{
//			#if UNITY_EDITOR
//			var output = PrefabUtility.InstantiatePrefab(assetComponentOrGameObject: Prefab,
//			                                             parent: InactiveParent) as GameObject;
//			#else
			var output = UnityEngine.Object.Instantiate(original: _prefab,
			                                            parent: InactiveParent);
//			#endif

			var result = output.GetComponentInChildren<IPoolItem>(includeInactive: true);

			if (ReferenceEquals(result,
			                    null))
			{
				#if debug
				LogWarning($"New instance of {typeof(PI).Name} didn't have a Component of that type anywhere in its hierarchy.");
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
			if (++_index >= _total) _index = 0;

			var item = _items[_index];

			Retire(item);
			
//			item.Retire();

//			item.Leased = true;
			
			item.transform.SetParent(_activeParent);
			
			item.gameObject.SetActive(true);

//			item.HandleGet();

			return item;
		}

		public void Retire(IPoolItem item)
		{
//			if (!item.Leased) return;

//			item.Leased = false;
			
			item.transform.SetParent(_inactiveParent);

			item.gameObject.SetActive(false);

//			item.HandleRetire();
		}


		public void RetireAll()
		{
			foreach (var item in Items)
			{
				Retire(item);
			}
		}

	}

}
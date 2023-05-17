//#if DEBUG_VANILLA_POOLS
//#define debug
//#endif
//
//using System;
//
////using Cysharp.Threading.Tasks;
//
//#if UNITY_EDITOR
//using UnityEditor;
//#endif
//
//using UnityEngine;
//
//using Object = UnityEngine.Object;
//
//namespace Vanilla.Pools
//{
//
//	public interface IPrefabStockAsyncPool<P, PI> : StockPool<P, PI>
//		where P : class, IPool<P, PI>
//		where PI : MonoBehaviour, IPoolItem<P,PI>
//	{
//
////		[SerializeField] protected Transform _inactiveParent;
////		public Transform InactiveParent
////		{
////			get => _inactiveParent;
////			set => _inactiveParent = value;
////		}
//
//		public Transform InactiveParent
//		{
//			get;
//			set;
//		}
//
////		[SerializeField] private Transform _activeParent;
////		public Transform ActiveParent
////		{
////			get => _activeParent;
////			set => _activeParent = value;
////		}
//		
//		public Transform ActiveParent
//		{
//			get;
//			set;
//		}
//
////		[SerializeField] protected GameObject _prefab;
////		public                     GameObject Prefab => _prefab;
//		
//		public GameObject Prefab
//		{
//			get;
//			set;
//		}
//		
//		new PI CreateItem()
//		{
//			#if UNITY_EDITOR
//			var output = PrefabUtility.InstantiatePrefab(assetComponentOrGameObject: Prefab,
//			                                             parent: InactiveParent) as GameObject;
//			#else
//			var output = UnityEngine.Object.Instantiate(original: _prefab,
//			                                            parent: InactiveParent);
//			#endif
//
//			return output.GetComponentInChildren<PI>(includeInactive: true);
//		}
//
//
//		void DestroyItem(PI item)
//		{
//			Object.Destroy(item.gameObject);
//			
//			return default;
//		}
//
//
//		public override async UniTask<PI> Get()
//		{
//			if (!Inactive.TryPop(out var item))
//			{
//				#if debug
//				LogWarning($"[{typeof(PI).Name}] pool exhausted!");
//				#endif
//				
//				return null;
//			}
//
//			if (item.Leased)
//			{
//				#if debug
//				LogWarning($"[{item}] was not returned to its [{typeof(PI).Name}] pool correctly.");
//				#endif
//			}
//
//			item.Leased = true;
//
//			Active.Add(item: item);
//
//			item.transform.SetParent(_activeParent);
//			
//			await item.HandleGet();
//
//			return item;
//		}
//
//
//		public override async UniTask Retire(PI item)
//		{
//			if (!item.Leased) return;
//
//			item.Leased = false;
//			
//			_active.Remove(item);
//
//			_inactive.Push(item);
//
//			item.transform.SetParent(p: _inactiveParent);
//
//			await item.HandleRetire();
//		}
//
//	}
//
//}
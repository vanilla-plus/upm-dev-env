#if DEBUG && POOLS
#define debug
#endif

using System;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

using static UnityEngine.Debug;

using Object = UnityEngine.Object;

namespace Vanilla.Pools
{

	[Serializable]
	public class Pool<PI> : IPool<PI>
		where PI : MonoBehaviour, IPoolItem
	{

		[SerializeField] private   int        _total    = 20;
		[SerializeField] protected List<PI>   _active   = new();
		[SerializeField] protected List<PI>  _inactive = new();
		[SerializeField] private   GameObject _prefab;
		[SerializeField] private   Transform  _inactiveParent;
		[SerializeField] private   Transform  _activeParent;

//		[Tooltip(tooltip: "If true, items will be automatically activated on Get and de-activated on Retire.")]
//		public bool toggleActiveOnAccess = true;

		public GameObject Prefab
		{
			get => _prefab;
			set => _prefab = value;
		}

		public Transform InactiveParent
		{
			get => _inactiveParent;
			set => _inactiveParent = value;
		}

		public Transform ActiveParent
		{
			get => _activeParent;
			set => _activeParent = value;
		}

		public int       Total    => _total;
		public List<PI>  Active   => _active ??= new List<PI>();
//		public Stack<PI> Inactive => _inactive ??= new Stack<PI>();
		public List<PI> Inactive => _inactive ??= new List<PI>();


		public virtual void Fill()
		{
			while (Active.Count + Inactive.Count < Total)
			{
				var newItem = Create();

				if (ReferenceEquals(objA: newItem,
				                    objB: null)) return;

				newItem.gameObject.name = $"{typeof(PI).Name} [{Inactive.Count}]";

				newItem.Pool = this as IPool<IPoolItem>;

				Inactive.Add(item: newItem);
			}
		}


		#if UNITY_EDITOR
		public virtual PI Create() => ((GameObject) PrefabUtility.InstantiatePrefab(assetComponentOrGameObject: _prefab,
		                                                                            parent: _inactiveParent)).GetComponentInChildren<PI>(true);
		#else
		public virtual PI Create() => Object.Instantiate(original: _prefab,
		                                                position: Vector3.zero,
		                                                rotation: Quaternion.identity,
		                                                parent: _parent).GetComponentInChildren<PI>(true);
		#endif


		public virtual void Drain()
		{
			while (Active.Count > 0)
			{
				Destroy(item: Active[0]);
				Active.RemoveAt(0);
			}

//			while (Inactive.Count > 0) Destroy(Inactive.Pop());

			while (Inactive.Count > 0)
			{
				var item = Inactive[0];
				
				Destroy(item: item);
				
				Inactive.RemoveAt(0);
			}

			_active.Clear();
			_inactive.Clear();
		}


		public void Destroy(PI item)
		{
			if (Application.isPlaying)
			{
				Object.Destroy(obj: item.gameObject);
			}
			else
			{
				Object.DestroyImmediate(obj: item.gameObject);
			}
		}


		public async UniTask<PI> Get()
		{
			if (Inactive.Count == 0)
			{
				#if debug
				LogWarning($"I'm fresh out of [{typeof(PI).Name}]s");
				#endif

				return null;
			}

//			var item = Inactive.Pop();

			var item = Inactive[0];
			
			Inactive.RemoveAt(0);

			Active.Add(item: item);

			#if debug
			LogWarning($"{typeof(PI).Name} Get! [{_inactive.Count}/{_total}] available.");
			#endif

			item.transform.SetParent(_activeParent);

//			if (toggleActiveOnAccess) item.gameObject.SetActive(value: true);

			await item.OnGet();

			return item;
		}


		public async UniTask Retire(PI item)
		{
			if (!_active.Contains(item: item)) return;

			_active.Remove(item);

//			_inactive.Push(item);

			_inactive.Add(item);

			var t = item.transform;

			t.SetParent(p: _inactiveParent);

//			if (resetTransformOnRetire)
//			{
//				t.position    = Vector3.zero;
//				t.eulerAngles = Vector3.zero;
//				t.localScale  = Vector3.one;
//			}

//			if (toggleActiveOnAccess)
//			{
//				item.gameObject.SetActive(value: false);
//			}

			await item.OnRetire();
		}


		public virtual async UniTask RetireAll()
		{
			while (Active.Count > 0) await Retire(Active[0]);
		}

	}

}
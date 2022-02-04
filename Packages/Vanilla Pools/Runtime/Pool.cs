#if DEBUG && POOLS
#define debug
#endif

using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

using static UnityEngine.Debug;

using Object = UnityEngine.Object;

namespace Vanilla.Pools
{

	[Serializable]
	public class Pool<P, I>
		where P : Pool<P, I>
		where I : PoolItem<P, I>
	{

		public GameObject prefab;

		public Transform sceneParent;

		public int total = 20;

		[Tooltip(tooltip: "If true, items will be automatically activated on Get and de-activated on Retire.")]
		public bool toggleActiveOnAccess = true;

		[Tooltip(tooltip: "If true, items will have their root transform reset upon Retire.")]
		public bool resetTransformOnRetire = true;

		[SerializeField]
		protected List<I> _available = new();
		public List<I> Available => _available ??= new List<I>();

		[SerializeField]
		protected List<I> _inUse = new();
		public List<I> InUse => _inUse ??= new List<I>();


		public virtual void Fill()
		{
			while (Available.Count + InUse.Count < total)
			{
				var newbie = Create();

				if (ReferenceEquals(objA: newbie,
				                    objB: null)) return;

				newbie.gameObject.name = $"{typeof(I).Name} [{Available.Count}]";

				newbie.Pool = this as P;

				Available.Add(item: newbie);
			}
		}


		#if UNITY_EDITOR
		public virtual I Create() => ((GameObject)PrefabUtility.InstantiatePrefab(assetComponentOrGameObject: prefab,
		                                                                          parent: sceneParent)).GetComponent<I>();
		#else
		public virtual I Create() => Object.Instantiate(original: prefab,
		                                                position: Vector3.zero,
		                                                rotation: Quaternion.identity,
		                                                parent: sceneParent).GetComponent<I>();
		#endif


		public virtual void Drain()
		{
			for (var i = Available.Count - 1;
			     i >= 0;
			     i--) DestroyItem(item: Available[index: i]);

			for (var i = InUse.Count - 1;
			     i >= 0;
			     i--) DestroyItem(item: InUse[index: i]);

			_available = new List<I>(capacity: total);
			_inUse     = new List<I>(capacity: total);
		}


		private void DestroyItem(I item)
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


		public I Get()
		{
			if (_available.Count < 1)
			{
				#if debug
				LogWarning($"I'm fresh out of [{typeof(I).Name}]s");
				#endif

				return null;
			}

			var item = _available[index: 0];

			_available.Remove(item: item);

			_inUse.Add(item: item);

			#if debug
			LogWarning($"{typeof(I).Name} Get! [{_available.Count}/{total}] available.");
			#endif

			if (toggleActiveOnAccess)
			{
				item.gameObject.SetActive(value: true);
			}

			item.OnGet();

			return item;
		}


		public void Retire(I item)
		{
			if (!_inUse.Contains(item: item)) return;

			_inUse.Remove(item: item);

			_available.Add(item: item);

			var t = item.transform;

			t.SetParent(p: sceneParent);

			if (resetTransformOnRetire)
			{
				t.position    = Vector3.zero;
				t.eulerAngles = Vector3.zero;
				t.localScale  = Vector3.one;
			}

			if (toggleActiveOnAccess)
			{
				item.gameObject.SetActive(value: false);
			}

			item.OnRetire();
		}


		public virtual void RetireAll()
		{
			for (var i = InUse.Count - 1;
			     i >= 0;
			     i--) Retire(item: InUse[index: i]);
		}

	}

}
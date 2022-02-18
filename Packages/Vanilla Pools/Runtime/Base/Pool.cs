#if DEBUG && POOLS
#define debug
#endif

using System;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using static UnityEngine.Debug;

namespace Vanilla.Pools
{

	[Serializable]
	public abstract class Pool<PI> : IPool<PI>
		where PI : MonoBehaviour, IPoolItem
	{

		[SerializeField] private   int        _total    = 20;
		[SerializeField] protected List<PI>   _active   = new(); // Could these be HashSet? HashTable?
		[SerializeField] protected List<PI>  _inactive = new();
		[SerializeField] protected Transform _inactiveParent;
		[SerializeField] private   Transform _activeParent;

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

		public int       Total
		{
			get => _total;
			set => _total = value;
		}

		public List<PI> Active   => _active ??= new List<PI>(capacity: Total);
		public List<PI> Inactive => _inactive ??= new List<PI>(capacity: Total);


		public virtual async UniTask CreateAll()
		{
			while (Active.Count + Inactive.Count < Total)
			{
				var newItem = await CreateNewItem();

				if (ReferenceEquals(objA: newItem,
				                    objB: null)) return;

				newItem.gameObject.name = $"{typeof(PI).Name} [{Inactive.Count}]";

				newItem.Pool = this as IPool<IPoolItem>;

				Inactive.Add(item: newItem);
			}
		}

		public virtual void DestroyAll()
		{
			while (Active.Count > 0)
			{
				DestroyItem(item: Active[0]);
				
				Active.RemoveAt(0);
			}

			while (Inactive.Count > 0)
			{
				DestroyItem(item: Inactive[0]);

				Inactive.RemoveAt(0);
			}

			_active.Clear();
			_inactive.Clear();
		}

		public abstract UniTask<PI> CreateNewItem();

		public abstract void DestroyItem(PI item);

		public async UniTask<PI> Get()
		{
			if (Inactive.Count == 0)
			{
				#if debug
				LogWarning($"[{typeof(PI).Name}] pool exhausted!");
				#endif

				return null;
			}

			var item = Inactive[0];
			
			Inactive.RemoveAt(0);

			Active.Add(item: item);

			item.transform.SetParent(_activeParent);

			item.LeasedFromPool = true;
			
			await item.OnGet();

			return item;
		}


		public async UniTask Retire(PI item)
		{
			if (!item.LeasedFromPool) return;

			item.LeasedFromPool = false;
			
			_active.Remove(item);

			_inactive.Add(item);

			item.transform.SetParent(p: _inactiveParent);

			await item.OnRetire();
		}


		public virtual async UniTask RetireAll()
		{
			while (Active.Count > 0) await Retire(Active[0]);
		}

	}

}
#if DEBUG && POOLS
#define debug
#endif

using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Pools
{

	[Serializable]
	public abstract class PoolItem : MonoBehaviour,
	                                 IPoolItem
	{

		[SerializeField] protected bool             _leasedFromPool;
		[SerializeField] protected IPool<IPoolItem> _pool;

		public bool LeasedFromPool
		{
			get => _leasedFromPool;
			set => _leasedFromPool = value;
		}

		public IPool<IPoolItem> Pool
		{
			get => _pool;
			set => _pool = value;
		}

		public abstract UniTask OnGet();

		public abstract UniTask OnRetire();

		public async UniTask RetireSelf() => await Pool.Retire(item: this);

	}

}
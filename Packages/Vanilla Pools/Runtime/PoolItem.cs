#if DEBUG && POOLS
#define debug
#endif

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Pools
{

	public abstract class PoolItem : MonoBehaviour,
	                                 IPoolItem
	{

		[SerializeField] protected IPool<IPoolItem> _pool;

		public IPool<IPoolItem> Pool
		{
			get => _pool;
			set => _pool = value;
		}

		public abstract UniTask OnGet();

		public abstract UniTask OnRetire();

		public async UniTask Retire() => await Pool.Retire(item: this);

	}

}
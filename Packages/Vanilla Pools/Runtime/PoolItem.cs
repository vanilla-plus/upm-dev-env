#if DEBUG && POOLS
#define debug
#endif

using UnityEngine;

namespace Vanilla.Pools
{

	public abstract class PoolItem<P, I> : MonoBehaviour
		where P : Pool<P, I>
		where I : PoolItem<P, I>
	{

		public abstract P Pool
		{
			get;
			set;
		}

		protected internal abstract void OnGet();
		
		protected internal abstract void OnRetire();

		public void Retire() => Pool.Retire(item: this as I);

	}

}
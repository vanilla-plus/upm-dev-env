#if DEBUG_VANILLA_POOLS
#define debug
#endif

using System.Collections.Generic;

using Cysharp.Threading.Tasks;

namespace Vanilla.Pools
{

//	public interface IPool<P, PI>
//		where P : class, IPool<P,PI>
//		where PI : class, IPoolItem<P,PI>
	public interface IPool
	{

		public int Total
		{
			get;
			set;
		}

		IPoolItem CreateItem();

		void CreateAll();

		void DestroyItem(IPoolItem item);

		void DestroyAll();

		IPoolItem Get();

		void Retire(IPoolItem item);

		void RetireAll();

	}

}
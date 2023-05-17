#if DEBUG_VANILLA_POOLS
#define debug
#endif

using System.Collections.Generic;

using Cysharp.Threading.Tasks;

namespace Vanilla.Pools
{

	public interface IPool<P, PI>
		where P : class, IPool<P,PI>
		where PI : class, IPoolItem<P,PI>
	{

		public int Total
		{
			get;
			set;
		}

		PI CreateItem();

		void CreateAll();

		void DestroyItem(PI item);

		void DestroyAll();

		PI Get();

		void Retire(PI item);

		void RetireAll();

	}

}
#if DEBUG_VANILLA_POOLS
#define debug
#endif

using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Pools
{
//	public interface IPoolItem<P,PI>
//		where P : class, IPool<P, PI>
//		where PI : class, IPoolItem<P, PI>
	public interface IPoolItem
	{

//		public bool Leased
//		{
//			get;
//			set;
//		}

		public IPool Pool
		{
			get;
			set;
		}
		
		GameObject gameObject
		{
			get;
		}
		
		Transform transform
		{
			get;
		}

//		void HandleGet();

//		void HandleRetire();

		void Retire() => Pool.Retire(item: this);

	}

}
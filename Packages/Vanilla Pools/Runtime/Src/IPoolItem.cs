using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Pools
{

	public interface IPoolItem
	{

		IPool<IPoolItem> Pool
		{
			get;
			set;
		}

		UniTask OnGet();

		UniTask OnRetire();

		UniTask Retire();

	}

}
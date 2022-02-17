using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Pools
{

	public interface IPool<PI>
		where PI : IPoolItem
	{

		int Total
		{
			get;
		}

		GameObject Prefab
		{
			get;
			set;
		}

		Transform InactiveParent
		{
			get;
			set;
		}
		
		Transform ActiveParent
		{
			get;
			set;
		}

		List<PI> Inactive
		{
			get;
		}

		List<PI> Active
		{
			get;
		}

		UniTask<PI> Get();

		UniTask Retire(PI item);
		
		PI Create();

		void Destroy(PI item);
		
		void Fill();

		void Drain();

		UniTask RetireAll();

	}

}
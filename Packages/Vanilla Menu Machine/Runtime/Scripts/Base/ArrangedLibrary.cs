using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Arrangement;
using Vanilla.Catalogue;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class ArrangedLibrary<C, CI, LI, PO, T, A, P> : Library<C, CI, LI, PO, T>
		where C : Catalogue<CI>
		where CI : CatalogueItem
		where LI : ArrangedLibraryItem<LI, CI, T>
		where PO : Pool<LI>
		where T : Transform
		where A : Arrangement<LI, T, P>
		where P : struct
	{

		[SerializeField] private A _arrangement;
		public                   A Arrangement => _arrangement;


		public override async UniTask Construct()
		{
			await base.Construct();

			_arrangement.Populate(parent: _pool.ActiveParent);

			_arrangement.InvokeArrangement();
		}

	}

}
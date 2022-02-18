using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Arrangement;
using Vanilla.Catalogue;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public class ArrangedLibrary<C, CI, PO, LI, T, A, P> : Library<C, CI, PO, LI, T>,
	                                                       IArrangedLibrary<C, CI, PO, LI, T, A, P>
		where C : Catalogue<CI>
		where CI : CatalogueItem
		where PO : Pool<LI>
		where LI : LibraryItem<CI, T>, IPoolItem, IArrangementItem
		where T : Transform
		where A : IArrangement<LI, T, P>
		where P : struct
	{

		[SerializeField] private A _arrangement;
		public                   A Arrangement => _arrangement;


		public override async UniTask Construct()
		{
			await base.Construct();

			_arrangement.Populate(parent: _pool.ActiveParent);

			_arrangement.AttemptArrange();
		}

	}

}
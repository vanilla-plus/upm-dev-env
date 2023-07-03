using System;

using UnityEngine;

using Vanilla.Arrangement;
using Vanilla.Catalogue;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class ArrangedLibrary3D<C, CI, LI, PO, A> : ArrangedLibrary<C, CI, LI, PO, Transform, A, Vector3>
		where C : Catalogue<CI>
		where CI : CatalogueItem
		where LI : ArrangedLibraryItem3D<LI, CI>, new()

		//		where PO : StockAsyncPool<,>
		where A : Arrangement3D<LI>
	{

        

	}

}
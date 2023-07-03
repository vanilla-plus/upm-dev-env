using System;

using UnityEngine;

using Vanilla.Arrangement;
using Vanilla.Catalogue;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class ArrangedLibrary2D<C, CI, LI, PO, A> : ArrangedLibrary<C, CI, LI, PO, RectTransform, A, Vector2>
		where C : Catalogue<CI>
		where CI : CatalogueItem
		where LI : ArrangedLibraryItem2D<LI, CI>, new()

		//		where PO : StockAsyncPool<,>
		where A : Arrangement2D<LI> { }

}
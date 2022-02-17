using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.Catalogue;
using Vanilla.Layout;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class Library2D<C, CI, LI, L, LOI, P> : Library<C, CI, LI, L, LOI, P, RectTransform, Vector2>
		where C : Catalogue<CI>
		where CI : CatalogueItem
		where LI : LibraryItem2D<CI>, LOI
		where L : Layout<LOI, RectTransform, Vector2>
		where LOI : ILayoutItem
		where P : Pool<LI>
	{

		

	}

}
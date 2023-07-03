using System;

using UnityEngine;

using Vanilla.Catalogue;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class ArrangedLibraryItem2D<LI, CI> : ArrangedLibraryItem<LI, CI, RectTransform>
		where LI : ArrangedLibraryItem2D<LI, CI>
		where CI : ICatalogueItem { }

}
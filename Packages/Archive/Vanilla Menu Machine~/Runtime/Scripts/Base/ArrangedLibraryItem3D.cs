using System;

using UnityEngine;

using Vanilla.Catalogue;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class ArrangedLibraryItem3D<LI,CI> : ArrangedLibraryItem<LI, CI, Transform>
		where LI : ArrangedLibraryItem3D<LI,CI>
		where CI : ICatalogueItem
	{

		

	}

}
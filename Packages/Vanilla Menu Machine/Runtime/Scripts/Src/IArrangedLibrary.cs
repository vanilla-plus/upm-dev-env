using UnityEngine;

using Vanilla.Catalogue;
using Vanilla.Pools;
using Vanilla.Arrangement;

namespace Vanilla.MediaLibrary
{

	public interface IArrangedLibrary<C, CI, PO, LI, T, A, P> : ILibrary<C, CI, PO, LI, T>
		where C : ICatalogue<CI>
		where CI : ICatalogueItem
		where PO : IPool<LI>
		where LI : MonoBehaviour, ILibraryItem<CI, T>, IPoolItem, IArrangementItem
		where T : Transform
		where A : IArrangement<LI, T, P>
		where P : struct
	{

		A Arrangement
		{
			get;
		}

	}

}
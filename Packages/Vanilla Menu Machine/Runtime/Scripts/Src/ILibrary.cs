using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Catalogue;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary
{
	
	public interface ILibrary<C, CI, PO, LI, T>
		where C : ICatalogue<CI>
		where CI : ICatalogueItem
		where PO : IPool<LI>
		where LI : MonoBehaviour, ILibraryItem<CI, T>, IPoolItem
		where T : Transform
	{

		C Catalogue
		{
			get;
		}

		PO Pool
		{
			get;
		}

		UniTask Construct();

	}

}
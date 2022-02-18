using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Catalogue;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class Library<C, CI, PO, LI, T> : MonoBehaviour,
	                                                 ILibrary<C, CI, PO, LI, T>

		where C : Catalogue<CI>
		where CI : CatalogueItem
		where PO : Pool<LI>
		where LI : LibraryItem<CI, T>, IPoolItem
		where T : Transform
	{

		public string fallbackJson;

		[SerializeField] protected C _catalogue;
		[SerializeField] protected PO _pool;

		public C Catalogue => _catalogue;
		public PO Pool      => _pool;

		/*
		Okay! Got some good ideas here. This Library class should have a couple of extra permutations:

		- Ecosphere-like
			- LibraryItem places itself (based on latlong)
			- Doesn't require arranging / dirty flag / 
			- Solution? Library shouldn't assume theres a layout. ArrangedLibrary should be a subclass/interface?

		- Non-GameObject based
			- LibraryItems arent represented with individual GameObjects
			- Also doesn't require arranging
			- Solution? Library shouldn't assume theres a pool. PooledLibrary should be a subclass/interface?

		- 

		*/


		protected virtual void Awake()
		{
			CatalogueBuilder.OnCatalogueFetchSuccess += HandleCatalogueFetchSuccess;

			CatalogueBuilder.FetchViaRemoteConfig<C, CI>(Catalogue,
			                                             fallbackJson);

			// The CatalogueBuilder should probably have already run by this point.
			// It should be one of the first things that happens.

			// In other words, OnCatalogueFetchSuccess has likely already happened.
			// And you could safely call HandleNewCatalogue directly.

//			HandleNewCatalogue();
		}


		public virtual void HandleCatalogueFetchSuccess() => Construct();


		public virtual async UniTask Construct()
		{
			foreach (var i in Catalogue.Items)
			{
				var item = await _pool.Get();

				await item.Populate(i);
			}
		}

	}

}
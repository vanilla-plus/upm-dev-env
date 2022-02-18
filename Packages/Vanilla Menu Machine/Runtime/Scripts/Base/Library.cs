using System;

using UnityEngine;

using Vanilla.Catalogue;
using Vanilla.Layout;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class Library<C, CI, LI, L, LOI, P, T, S> : MonoBehaviour,
		                                                       ILibrary<C, CI, LI, L, LOI, P, T, S>
		where C : Catalogue<CI>
		where CI : CatalogueItem
		where LI : LibraryItem<CI, T>, LOI, IPoolItem
		where L : Layout<LOI, T, S>
		where LOI : ILayoutItem
		where P : Pool<LI>
		where T : Transform
		where S : struct
	{

		public string fallbackJson;

		[SerializeField] protected C _catalogue;
		[SerializeField] protected L _layout;
		[SerializeField] protected P _pool;

		public C Catalogue
		{
			get => _catalogue;
			set => _catalogue = value;
		}

		public L Layout    => _layout;
		public P Pool      => _pool;

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
			CatalogueBuilder.OnCatalogueFetchSuccess += HandleNewCatalogue;
			
			CatalogueBuilder.FetchViaRemoteConfig<C,CI>(Catalogue,
														fallbackJson);

			// The CatalogueBuilder should probably have already run by this point.
			// It should be one of the first things that happens.

			// In other words, OnCatalogueFetchSuccess has likely already happened.
			// And you could safely call HandleNewCatalogue directly.

//			HandleNewCatalogue();
		}


		protected virtual async void HandleNewCatalogue()
		{
			foreach (var i in Catalogue.Items)
			{				
				var item = await _pool.Get();

				await item.Populate(i);
			}

			_layout.Populate(parent: _pool.ActiveParent);
			
			_layout.AttemptArrange();
		}

	}

}
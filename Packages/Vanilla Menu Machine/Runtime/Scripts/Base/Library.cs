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

		[SerializeField] private   C _catalogue;
		[SerializeField] private   L _layout;
		[SerializeField] protected P _pool;

		public C Catalogue
		{
			get => _catalogue;
			set => _catalogue = value;
		}

		public L Layout    => _layout;
		public P Pool      => _pool;


		protected virtual void Awake()
		{
			CatalogueBuilder.OnCatalogueFetchSuccess += HandleNewCatalogue;

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
				Debug.LogError("What");
				
				var item = await _pool.Get();

				item.CatalogueItem = i;
			}

			_layout.Populate(parent: _pool.ActiveParent);
			
			_layout.AttemptArrange();
		}

	}

}
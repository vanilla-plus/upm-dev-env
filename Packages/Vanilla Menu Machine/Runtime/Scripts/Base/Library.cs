using System;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Catalogue;
using Vanilla.Pools;

using static UnityEngine.Debug;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class Library<C, CI, LI, PO, T> : MonoBehaviour
		where C : Catalogue<CI>
		where CI : CatalogueItem
		where LI : LibraryItem<LI, CI, T>
		where PO : Pool<LI>
		where T : Transform
	{

		public string fallbackJson;

		[SerializeField] protected C        _catalogue;
		[SerializeField] protected List<LI> _items;
		[SerializeField] protected PO       _pool;
//		[SerializeField] protected bool     _monoSelectable = true;
		[SerializeField] protected bool _focusOnSelected = true;
		[SerializeField] protected bool _focusInProgress = false;

		public C        Catalogue       => _catalogue;
		public List<LI> Items           => _items;
		public PO       Pool            => _pool;
//		public bool     MonoSelectable  => _monoSelectable;
		public bool FocusOnSelected => _focusOnSelected;
		public bool FocusInProgress => _focusInProgress;

//		public LI             Selected;
//		public static Action<LI, LI> OnSelected;

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
			
			// This is weird!
			// Is there really no way to connect HandleMonoSelection to LI.OnSelected here..?
			// As far as I can tell, it has to happen in the finalized class... how annoying!

			SubscribeToMonoSelection();
		}


		protected abstract void SubscribeToMonoSelection();

		public virtual void HandleCatalogueFetchSuccess() => Construct();


		public virtual async UniTask Construct()
		{
			Log(message: "Converting catalogue into pool items");

			// Retire all current items here?
			// Clear Item list?

			_items.Clear();

			foreach (var i in Catalogue.Items)
			{
				var item = await _pool.Get();

				await item.Populate(item: i);

				_items.Add(item: item);
			}

//			foreach (var i in _items)
//			{
//				i.PointerSelected.toggle.onTrue += () =>
//				                                   {
//					                                   if (!_monoSelectable) return;
//
//					                                   // If the thing that was just selected is the same as what is already selected,
//					                                   // drop out early.
//					                                   if (ReferenceEquals(objA: i,
//					                                                       objB: Selected)) return;
//
//					                                   var outgoing = Selected;
//					                                   
//					                                   // If there is an outgoing Selected item, tell it it is no longer selected.
//
//					                                   if (!ReferenceEquals(objA: outgoing,
//					                                                        objB: null)) outgoing.PointerSelected.toggle.State = false;
//
//					                                   Selected = i;
//
//					                                   OnSelected?.Invoke(arg1: outgoing,
//					                                                      arg2: i);
//				                                   };
//			}

		}


		protected virtual void HandleMonoSelection(LI outgoing,
		                                           LI incoming)
		{
			if (!_focusOnSelected) return;

			if (ReferenceEquals(objA: incoming,
			                    objB: null)) return;

			FocusAsync(item: incoming,
			           selectedToggle: incoming.PointerSelected.Toggle);
		}


		protected virtual async UniTask FocusAsync(LI item,
		                                           Toggle selectedToggle)
		{
			_focusInProgress = true;
			
			while (FocusWhile(item: item,
			                      selectedToggle: selectedToggle))
			{
				FocusFrame(item: item);

				await UniTask.Yield();
			}

			// If the items selected toggle is still true by this point, it means that we got to focus all the way
			// onto this item without interruption from another selection.
			
			if (selectedToggle.State)
			{
				_focusInProgress = false;
			}
		}

		protected abstract void FocusFrame(LI item);


		protected abstract bool FocusWhile(LI item,
		                                   Toggle selectedToggle);

	}

}
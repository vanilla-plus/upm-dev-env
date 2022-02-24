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

		[Header(header: "Focus Settings")]
		[SerializeField] protected bool _focusOnPointerHover;
		[SerializeField] protected bool _focusOnPointerDown;
		[SerializeField] protected bool _focusOnPointerSelect = true;

		[Header(header: "Focus State")]
		[SerializeField] protected Toggle _focusInProgress = new(startingState: false);
		[SerializeField] protected LI     _focusTarget;
		[SerializeField] protected T      _focusTargetTransform;

		[Header(header: "Await Settings")]
		[SerializeField] protected bool _awaitItemPopulation;
		[SerializeField] protected int _awaitItemPopulationStaticDelay = 500;
		[SerializeField] protected bool  _awaitHandleNewItem;
		[SerializeField] protected int _awaitHandleNewItemStaticDelay = 500;


		public C        Catalogue            => _catalogue;
		public List<LI> Items                => _items;
		public PO       Pool                 => _pool;
		public bool     FocusOnPointerHover  => _focusOnPointerHover;
		public bool     FocusOnPointerDown   => _focusOnPointerDown;
		public bool     FocusOnPointerSelect => _focusOnPointerSelect;
		public Toggle   FocusInProgress      => _focusInProgress;
		public LI       FocusTarget          => _focusTarget;
		public T        FocusTargetTransform => _focusTargetTransform;


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

//			SubscribeToMonoSelection();

			FocusInProgress.onTrue += () => FocusAsync();
		}


//		protected abstract void SubscribeToMonoSelection();

		public virtual void HandleCatalogueFetchSuccess() => Construct();


		public virtual async UniTask Construct()
		{

			// ------------------------------------------------------- Retire items that are no longer present

			Log(message: "Converting catalogue into pool items");

			// Retire all current items here?
			// Clear Item list?

			_items.Clear();
			
			// ------------------------------------------------------- Add new items

			foreach (var i in Catalogue.Items)
			{
				var item = await _pool.Get();

				_items.Add(item: item);

				if (ReferenceEquals(objA: _focusTarget,
				                    objB: null))
				{
					_focusTarget          = item;
					_focusTargetTransform = _focusTarget.Transform;
				}

				// Weird idea - could have a toggle here for flipping the order Populate and Handle happen in.
				// It might come up that Populate needs to happen afterwards or vice versa?
				
				if (_awaitItemPopulation)
				{
					await item.Populate(item: i);
				}
				else
				{
					// It may be that we don't want to wait for items to populate, but we still want an artificial
					// delay between each population anyway. This amount is _awaitItemPopulateStaticDelay in milliseconds.
					
					// The same is true for _awaitItemHandleStaticDelay below.

					item.Populate(item: i);
					
					if (_awaitItemPopulationStaticDelay > 0)
					{
						await UniTask.Delay(millisecondsDelay: _awaitItemPopulationStaticDelay);
					}
				}

				if (_awaitHandleNewItem)
				{
					await HandleNewItem(newItem: item);
				}
				else
				{
					HandleNewItem(newItem: item);

					if (_awaitHandleNewItemStaticDelay > 0)
					{
						await UniTask.Delay(millisecondsDelay: _awaitHandleNewItemStaticDelay);
					}
				}
			}

			// -------------------------------------------------------- Subscriptions

//			if (_focusOnPointerHover)
//				foreach (var i in Items)
//					i.PointerHover.Toggle.onChange += b => InvokeFocus();
//			
//			if (_focusOnPointerDown)
//				foreach (var i in Items)
//					i.PointerDown.Toggle.onChange += b => InvokeFocus();
//			
//			if (_focusOnPointerSelect)
//				foreach (var i in Items)
//					i.PointerSelected.Toggle.onChange += b => InvokeFocus();
//
//			foreach (var i in Items) i.PointerSelected.Toggle.onTrue += () =>
//			                                                            {
//				                                                            _focusTarget          = i;
//				                                                            _focusTargetTransform = _focusTarget.Transform;
//			                                                            };

//			if (_items.Count > 0)
//			{
//				_focusTarget = _items[0];
//				_focusTargetTransform = _items[0].Transform;
//			}

		}


		public virtual async UniTask HandleNewItem(LI newItem)
		{
			if (_focusOnPointerHover)
			{
				newItem.PointerHover.Normal.Empty.onChange += _ => FocusInProgress.State = true;
				newItem.PointerHover.Normal.Full.onChange  += _ => FocusInProgress.State = true;
				
//				newItem.PointerHover.Normal.OnChange += f => InvokeFocus();
			}

			if (_focusOnPointerDown)
			{
				newItem.PointerDown.Normal.Empty.onChange += _ => FocusInProgress.State = true;
				newItem.PointerDown.Normal.Full.onChange  += _ => FocusInProgress.State = true;
				
//				newItem.PointerDown.Normal.OnChange += f => InvokeFocus();
			}

			if (_focusOnPointerSelect)
			{
				newItem.PointerSelected.Normal.Empty.onChange += _ => FocusInProgress.State = true;
				newItem.PointerSelected.Normal.Full.onChange  += _ => FocusInProgress.State = true;

//				newItem.PointerSelected.Normal.OnChange += f => InvokeFocus();
			}

			newItem.PointerSelected.Toggle.onTrue += () =>
			                                         {
				                                         if (ReferenceEquals(objA: _focusTarget,
				                                                             objB: newItem)) return;
				                                         
				                                         _focusTarget          = newItem;
				                                         _focusTargetTransform = _focusTarget.Transform;
			                                         };
		}


//		protected virtual void HandleMonoSelection(LI outgoing,
//		                                           LI incoming)
//		{
//			if (!_focusOnSelected) return;
//
//			if (ReferenceEquals(objA: incoming,
//			                    objB: null)) return;
//
//			InvokeFocus(incoming);
//		}


//		protected virtual void InvokeFocus()
//		{
			// Lets allow ourselves to swap target without needing to re-invoke the FocusAsync UniTask.
			// If we use SmoothDamp to slide/move whatever we need to move, the transition will be nice and seamless.

			
//			_focusTarget          = newTarget;

//			if (ReferenceEquals(objA: newTarget,
//			                    objB: null))
//			{
//				_focusTargetTransform = null;
//				
//				return;
//			}

//			_focusTargetTransform = _focusTarget.Transform;

//			if (_focusInProgress) return;

//			_focusInProgress = true;
			
//			FocusAsync();
//		}


		protected virtual async UniTask FocusAsync()
		{
			do
			{
				FocusFrame();

				await UniTask.Yield();
			}
			while (FocusWhile());
			
//			Debug.LogError(message: $"FocusWhile is now false? [{FocusWhile()}]");

			FocusInProgress.State   = false;

//			_focusInProgress = false;
		}

		protected abstract void FocusFrame();


		protected abstract bool FocusWhile();

	}

}
using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.EventSystems;

using Vanilla.Catalogue;
using Vanilla.Layout;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class LibraryItem<CI, T> : MonoBehaviour,
	                                           ILibraryItem<CI>,
	                                           ILayoutItem,
	                                           IPointerEnterHandler,
	                                           IPointerExitHandler,
	                                           IPointerDownHandler,
	                                           IPointerUpHandler,
	                                           IPoolItem
		where CI : ICatalogueItem
		where T : Transform
	{

		// ------------------------------------------------------------------------------------------------------------------------ MonoBehaviour //

		[SerializeField] private T _transform;
		public                   T Transform => _transform;

		protected virtual void OnValidate() => _transform = transform as T;

		protected virtual void Awake()
		{
			_pointerHover.Init();
			_pointerDown.Init();
			_pointerSelected.Init();
		}

		// --------------------------------------------------------------------------------------------------------------------------- LayoutItem //

		[SerializeField] private Toggle _dirty = new Toggle(startingState: false);
		public                   Toggle Dirty => _dirty;

		// ----------------------------------------------------------------------------------------------------------------------------- PoolItem //

		[SerializeField]
		private IPool<IPoolItem> _pool;
		public IPool<IPoolItem> Pool
		{
			get => _pool;
			set => _pool = value;
		}

		public abstract UniTask OnGet();

		public abstract UniTask OnRetire();

		public abstract UniTask Retire();

		// -------------------------------------------------------------------------------------------------------------------------- LibraryItem //

		[SerializeField] private CI _catalogueItem;
		public CI CatalogueItem
		{
			get => _catalogueItem;
			set => _catalogueItem = value;
		}
		
//		[SerializeField] private Toggle _pointerHover    = new Toggle(false);
//		[SerializeField] private Toggle _pointerDown     = new Toggle(false);
//		[SerializeField] private Toggle _pointerSelected = new Toggle(false);
		
//		[SerializeField] private Normal _pointerHoverNormal    = new Normal(startingValue: 0.0f);
//		[SerializeField] private Normal _pointerDownNormal     = new Normal(startingValue: 0.0f);
//		[SerializeField] private Normal _pointerSelectedNormal = new Normal(startingValue: 0.0f);

//		public Toggle PointerHover    => _pointerHover;
//		public Toggle PointerDown     => _pointerDown;
//		public Toggle PointerSelected => _pointerSelected;
		
//		public Normal PointerHoverNormal    => _pointerHoverNormal;
//		public Normal PointerDownNormal     => _pointerDownNormal;
//		public Normal PointerSelectedNormal => _pointerSelectedNormal;

		[SerializeField]
		private State _pointerHover = new State(startingState: false,
		                                        startingValue: 0.0f);
		[SerializeField]
		private State _pointerDown = new State(startingState: false,
		                                       startingValue: 0.0f);
		[SerializeField]
		private State _pointerSelected = new State(startingState: false,
		                                           startingValue: 0.0f);
		
		public State PointerHover    => _pointerHover;
		public State PointerDown     => _pointerDown;
		public State PointerSelected => _pointerSelected;
		
		public abstract UniTask Populate(CI item);
		
		public void OnPointerEnter(PointerEventData eventData) => _pointerHover.toggle.State = true;


		public void OnPointerExit(PointerEventData eventData)
		{
			_pointerHover.toggle.State = false;

			_pointerDown.toggle.State = false;
		}


		public void OnPointerDown(PointerEventData eventData) => _pointerDown.toggle.State = true;


		public void OnPointerUp(PointerEventData eventData)
		{
			if (_pointerDown.toggle)
			{
				_pointerDown.toggle.State = false;

				_pointerSelected.toggle.Flip();
			}
		}
		
	}

}
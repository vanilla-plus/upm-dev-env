using System;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.EventSystems;

using Vanilla.Catalogue;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class LibraryItem<CI, T> : MonoBehaviour,
	                                           ILibraryItem<CI, T>
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

		[SerializeField] private Toggle _dirty = new(startingState: false);
		public                   Toggle Dirty => _dirty;

		// ----------------------------------------------------------------------------------------------------------------------------- PoolItem //

		[SerializeField] private bool             _leasedFromPool;
		[NonSerialized]  private IPool<IPoolItem> _pool;

		public bool LeasedFromPool
		{
			get => _leasedFromPool;
			set => _leasedFromPool = value;
		}

		public IPool<IPoolItem> Pool
		{
			get => _pool;
			set => _pool = value;
		}

		public abstract UniTask OnGet();

		public abstract UniTask OnRetire();

		public virtual async UniTask RetireSelf() => await _pool.Retire(item: this);

		// -------------------------------------------------------------------------------------------------------------------------- LibraryItem //

		[SerializeField] private CI _payload;


		[SerializeField] private State _pointerHover = new(startingState: false,
		                                                   startingValue: 0.0f);

		[SerializeField] private State _pointerDown = new(startingState: false,
		                                                  startingValue: 0.0f);

		[SerializeField] private State _pointerSelected = new(startingState: false,
		                                                      startingValue: 0.0f);

		public CI Payload
		{
			get => _payload;
			set => _payload = value;
		}

		public State PointerHover    => _pointerHover;
		public State PointerDown     => _pointerDown;
		public State PointerSelected => _pointerSelected;

		public abstract UniTask Populate(CI item);

		// --------------------------------------------------------------------------------------------------------------- IPointerRedirectTarget //

		public void PointerRedirectEnter(PointerEventData eventData) => _pointerHover.toggle.State = true;

		public void PointerRedirectExit(PointerEventData eventData) => _pointerHover.toggle.State = _pointerDown.toggle.State = false;

		public void PointerRedirectDown(PointerEventData eventData) => _pointerDown.toggle.State = true;


		public void PointerRedirectUp(PointerEventData eventData)
		{
			if (!_pointerDown.toggle) return;

			_pointerDown.toggle.State = false;

			_pointerSelected.toggle.Flip();
		}

	}

}
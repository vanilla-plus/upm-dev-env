using System;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Vanilla.Catalogue;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class LibraryItem<LI, CI, T> : MonoBehaviour,
	                                               IPoolItem,
	                                               IPointerEnterHandler,
	                                               IPointerExitHandler,
	                                               IPointerDownHandler,
	                                               IPointerUpHandler
//	                                               IPointerRedirectTarget
		where LI : LibraryItem<LI, CI, T>
		where CI : ICatalogueItem
		where T : Transform
	{

		// -------------------------------------------------------------------------------------------------------------------------- Ad-Hoc Text //

		[Serializable]
		private struct AdHocText
		{

			[SerializeField]
			private Text   text;
			
			[SerializeField]
			private string key;

//			public void Populate(JToken data) => text.text = (string) data[key];
			public void Populate(CI item) => text.text = (string) item.Data[key];
			
			public void Populate(string dataString) => text.text = dataString;

		}
		
		// ------------------------------------------------------------------------------------------------------------------------ MonoBehaviour //

		[SerializeField] private T _transform;
		public                   T Transform => _transform;


		protected virtual void OnValidate()
		{
			_transform = transform as T;

			_pointerHover.OnValidate();
			_pointerDown.OnValidate();
			_pointerSelected.OnValidate();
		}


		protected virtual void Awake()
		{
			_pointerHover.Init();
			_pointerDown.Init();
			_pointerSelected.Init();

			_pointerSelected.Active.onTrue += () =>
			                                  {
				                                  if (!IsMonoSelectable()) return;

				                                  // Can't be selected twice!

				                                  if (ReferenceEquals(objA: this,
				                                                      objB: Selected)) return;

				                                  var outgoing = Selected;

				                                  // If the old guy exists, tell him he's fired

				                                  if (!ReferenceEquals(objA: outgoing,
				                                                       objB: null)) outgoing.PointerSelected.Active.Value = false;

				                                  Selected = (LI) this;

				                                  OnSelectedChange?.Invoke(arg1: outgoing,
				                                                           arg2: Selected);
			                                  };
		}


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


		// If we want this class to not be mono-selectable (multiple items can be selected at a time)
		// make sure to set it to false in a child class.
		public virtual bool IsMonoSelectable() => true;

		public static LI Selected;

		public static Action<LI, LI> OnSelectedChange;

		[SerializeField] private CI _payload;

		[SerializeField] private SmartState.SmartState _pointerHover = new(startingState: false);

		[SerializeField] private SmartState.SmartState _pointerDown = new(startingState: false);

		[SerializeField] private SmartState.SmartState _pointerSelected = new(startingState: false);

		public CI Payload
		{
			get => _payload;
			set => _payload = value;
		}

		public SmartState.SmartState PointerHover    => _pointerHover;
		public SmartState.SmartState PointerDown     => _pointerDown;
		public SmartState.SmartState PointerSelected => _pointerSelected;


		public virtual UniTask Populate(CI item)
		{
//			foreach (var t in adhocTexts) t.Populate(data: item.RawData);
			foreach (var t in adhocTexts) t.Populate(item);

			return default;
		}

		// --------------------------------------------------------------------------------------------------------------- Adhoc Text //

		[SerializeField]
		private AdHocText[] adhocTexts = Array.Empty<AdHocText>();

		// --------------------------------------------------------------------------------------------------------------- IPointerRedirectTarget //

//		public void PointerRedirectEnter(PointerEventData eventData) => _pointerHover.Toggle.State = true;
//
//		public void PointerRedirectExit(PointerEventData eventData) => _pointerHover.Toggle.State = _pointerDown.Toggle.State = false;
//
//		public void PointerRedirectDown(PointerEventData eventData) => _pointerDown.Toggle.State = true;
//
//
//		public void PointerRedirectUp(PointerEventData eventData)
//		{
//			if (!_pointerDown.Toggle) return;
//
//			_pointerDown.Toggle.State = false;
//
//			if (IsMonoSelectable())
//			{
//				_pointerSelected.Toggle.State = true;
//
//				// Can't unselect the same item by clicking on it?
//				// Wouldn't that insinuate that 'nothing' has become selected in that case?
//				// If we wanted, we could set Selected to null here...
//			}
//			else
//			{
//				_pointerSelected.Toggle.Flip();
//			}
//
//		}

		public void OnPointerEnter(PointerEventData eventData) => _pointerHover.Active.Value = true;

		public void OnPointerExit(PointerEventData eventData) => _pointerHover.Active.Value = _pointerDown.Active.Value = false;

		public void OnPointerDown(PointerEventData eventData) => _pointerDown.Active.Value = true;

		public void OnPointerUp(PointerEventData eventData)
		{
			if (!_pointerDown.Active) return;

			_pointerDown.Active.Value = false;

			if (IsMonoSelectable())
			{
				_pointerSelected.Active.Value = true;

				// Can't unselect the same item by clicking on it?
				// Wouldn't that insinuate that 'nothing' has become selected in that case?
				// If we wanted, we could set Selected to null here...
			}
			else
			{
				_pointerSelected.Active.Flip();
			}
		}

	}

}
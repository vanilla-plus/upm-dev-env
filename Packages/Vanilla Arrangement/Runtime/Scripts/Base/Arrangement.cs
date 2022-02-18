using System;
using System.Linq;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Arrangement
{

	[Serializable]
	public abstract class Arrangement<I, T, P> : IArrangement<I, T, P>
		where I : IArrangementItem
		where T : Transform
		where P : struct
	{

		[SerializeField]
		protected I[] _items;
		public I[] Items
		{
			get => _items;
			set => _items = value;
		}
		
		[SerializeField]
		protected T[] _transforms;
		public T[] Transforms
		{
			get => _transforms;
			set => _transforms = value;
		}

		[SerializeField]
		private bool _arrangementInProgress = false;
		public bool ArrangementInProgress => _arrangementInProgress;

		private Action _onArrangeBegun;
		public Action OnArrangeBegun
		{
			get => _onArrangeBegun;
			set => _onArrangeBegun = value;
		}

		private Action _onArrangeComplete;
		public Action OnArrangeComplete
		{
			get => _onArrangeComplete;
			set => _onArrangeComplete = value;
		}

		public void Populate(Transform parent)
		{
			var count = parent.childCount;

			_items      = new I[count];
			_transforms = new T[count];

			for (var i = 0;
			     i < count;
			     i++)
			{
				var t = parent.GetChild(index: i);

				_items[i]      = t.GetComponent<I>();
				_transforms[i] = t as T;

				_items[i].Dirty.onTrue += AttemptArrange;
			}
		}

		public void ArrangeItems()
		{
			if (_transforms        == null
			 || _transforms.Length == 0) return;

			T prev    = _transforms[0];
			T current = prev;
			
			ArrangeItem(target: current,
			            position: GetInitialPosition());

			if (_transforms.Length == 1) return;

			for (var i = 1;
			     i < _transforms.Length;
			     i++)
			{
				current = _transforms[i];

				ArrangeItem(target: current,
				            position: GetNewPosition(prev: prev,
				                                     current: current));

				prev = current;
			}
		}


		public void AttemptArrange()
		{
			if (_arrangementInProgress) return;

			_arrangementInProgress = true;

			ArrangeAsync();
		}
		
		protected async UniTask ArrangeAsync()
		{
			OnArrangeBegun?.Invoke();

			do
			{
				ArrangeItems();

				await UniTask.Yield();
			}
			while (ArrangementRequired());

			OnArrangeComplete?.Invoke();

			_arrangementInProgress = false;
		}


		public bool ArrangementRequired() => _items.Any(i => i.Dirty);

		public abstract void ArrangeItem(T target,
		                                 P position);


		public abstract P GetInitialPosition();


		public abstract P GetNewPosition(T prev,
		                                 T current);


		public abstract P GetPreviousOffset(T prev);

		public abstract P GetCurrentOffset(T current);

	}

}
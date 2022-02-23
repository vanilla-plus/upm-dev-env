using System;
using System.Collections.Generic;
using System.Linq;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Arrangement
{

	[Serializable]
	public abstract class Arrangement<I, T, P> : IArrangement<I, T, P>
		where I : class, IArrangementItem<T>
		where T : Transform
		where P : struct
	{

		[SerializeField]
		private T _parent;
		public  T Parent => _parent;

		[SerializeField]
		protected HashSet<I> _items;
		public HashSet<I> Items
		{
			get => _items;
			set => _items = value;
		}

		[SerializeField]
		private bool _forceArrangement = false;
		public bool ForceArrangement
		{
			get => _forceArrangement;
			set => _forceArrangement = value;
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


		public void Populate()
		{
			if (_items == null) _items = new HashSet<I>(capacity: 64);

			for (var i = 0;
			     i < _parent.childCount;
			     i++)
			{
				var c = _parent.GetChild(index: i).GetComponent<I>();

				if (_items.Contains(item: c)) continue;

				_items.Add(item: c);

				c.ArrangementDirty.onTrue += InvokeArrangement;
			}
		}


		public virtual void ArrangeFrame()
		{
			I prev = null;
			
			foreach (var c in _items)
			{
				ArrangeItem(current: c,
				            position: prev == null ?
					                      GetInitialPosition(c) :
					                      GetNewPosition(prev: prev,
					                                     current: c));
				
				prev = c;
			}
		}

		public void InvokeArrangement()
		{
			if (_arrangementInProgress) return;

			_arrangementInProgress = true;

			ArrangeAsync();
		}
		
		private async UniTask ArrangeAsync()
		{
			OnArrangeBegun?.Invoke();

			do
			{
				ArrangeFrame();

				await UniTask.Yield();
			}
			while (ArrangementRequired());

			OnArrangeComplete?.Invoke();

			_arrangementInProgress = false;
		}


		public bool ArrangementRequired() => _forceArrangement || _items.Any(predicate: i => i.ArrangementDirty);

		public abstract void ArrangeItem(I current,
		                                 P position);


		public abstract P GetInitialPosition(I current);


		public abstract P GetNewPosition(I prev,
		                                 I current);


		public abstract P GetPreviousOffset(I prev);

		public abstract P GetCurrentOffset(I current);

	}

}
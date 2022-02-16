using System;
using System.Linq;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Layout
{

	[Serializable]
	public abstract class LayoutBase<I, T, P> : ILayout<I, T, P>
		where I : Component, ILayoutItem<T>
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

//
//		public LayoutBase() => OnArrangeComplete += () =>
//		                                            {
//			                                            foreach (var t in _transforms)
//			                                            {
//				                                            t.hasChanged = false;
//			                                            }
//		                                            };


		public void Populate(Transform parent)
		{
			OnArrangeComplete += () =>
			                     {
				                     foreach (var t in _transforms)
				                     {
					                     t.hasChanged = false;
				                     }
			                     };
			
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
				
//				_items[i].Transform = parent.GetChild(index: i) as T;
			}
		}


		public void ArrangeItems()
		{
			if (_transforms        == null
			 || _transforms.Length == 0) return;

			ArrangeItem(target: _transforms[0],
			            position: GetInitialPosition());

			if (_transforms.Length == 1) return;

			T current = null;
			T prev    = _transforms[0];

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
		
		public async UniTask ArrangeAsync()
		{
			OnArrangeBegun?.Invoke();

			do
			{
				ArrangeItems();

				await UniTask.Yield();
			}
			while (ArrangementRequired());

			await UniTask.Yield();

			await UniTask.Delay(500);

			foreach (var t in Transforms) t.hasChanged = false;

			OnArrangeComplete?.Invoke();

			_arrangementInProgress = false;
		}


//		public bool ArrangementRequired() => _items.Any(i => i.IsDirty);
		public bool ArrangementRequired() => _transforms.Any(t => t.hasChanged); // Does this work the same as a dirty flag but automatically?

		/* From Catlike
		Transform has a hasChanged property which is set to true whenever one of its values is changed.
		So it is useful to check whether the position, rotation, or scale has been modified since the last time you checked.

		However, nobody sets hasChanged back to false, because there is no universal moment when this should happen. It is up to you to reset it.
		*/
		
		// ToDo - swap back to your own custom IsDirty flag, or mess around trying to understand hasChanged. It doesn't actually seem to work,
		// ToDo - even if you do follow the above advice.

		public abstract void ArrangeItem(T target,
		                                 P position);


		public abstract P GetInitialPosition();


		public abstract P GetNewPosition(T prev,
		                                 T current);


		public abstract P GetPreviousOffset(T prev);

		public abstract P GetCurrentOffset(T current);

	}

}
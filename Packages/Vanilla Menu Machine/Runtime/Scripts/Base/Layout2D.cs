using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.MenuMachine
{

	[Serializable]
	public abstract class Layout2D<L> : ILayout<L, LayoutItem2D, RectTransform, Vector2>
		where L : Layout2D<L>
	{
		
		[SerializeField]
		private LayoutItem2D[] _items;
		public LayoutItem2D[] Items
		{
			get => _items;
			set => _items = value;
		}

		public Layout2D(LayoutItem2D[] items)
		{
			_items = items;
			
			for (var i = 1;
			     i < _items.Length;
			     i++)
			{
				_items[i]._previous = _items[i - 1].Transform;
			}
		}

		public void Arrange()
		{
			foreach (var i in _items)
			{
				i.Transform.anchoredPosition = ReferenceEquals(objA: i.Previous,
				                                               objB: null) ?
					                               GetInitialPosition() :
					                               i.Previous.anchoredPosition + GetPreviousOffset(i.Previous) + GetCurrentOffset(i.Transform);
			}
		}


		public virtual Vector2 GetInitialPosition() => _items[0].Transform.anchoredPosition;

		public abstract Vector2 GetPreviousOffset(RectTransform prev);

		public abstract Vector2 GetCurrentOffset(RectTransform current);

	}

}
using System;

using UnityEngine;

namespace Vanilla.Arrangement
{

	[Serializable]
	public abstract class Arrangement2D<I> : Arrangement<I, RectTransform, Vector2>
		where I : class, IArrangementItem<RectTransform>
	{

		public override void ArrangeItem(I current,
		                                 Vector2 position) => current.Transform.anchoredPosition = position;


		public override Vector2 GetInitialPosition(I current) => current.Transform.anchoredPosition;


		public override Vector2 GetNewPosition(I prev,
		                                       I current) => prev.Transform.anchoredPosition + GetPreviousOffset(prev: prev) + GetCurrentOffset(current: current);






	}

}
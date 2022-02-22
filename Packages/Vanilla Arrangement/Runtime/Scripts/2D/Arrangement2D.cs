using System;

using UnityEngine;

namespace Vanilla.Arrangement
{

	[Serializable]
	public abstract class Arrangement2D<I> : Arrangement<I, RectTransform, Vector2>
		where I : IArrangementItem
	{

		public override void ArrangeItem(RectTransform target,
		                                 Vector2 position) => target.anchoredPosition = position;


		public override Vector2 GetInitialPosition() => _transforms[0].anchoredPosition;


		public override Vector2 GetNewPosition(RectTransform prev,
		                                       RectTransform current) => prev.anchoredPosition + GetPreviousOffset(prev: prev) + GetCurrentOffset(current: current);






	}

}
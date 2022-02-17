using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Layout
{

	[Serializable]
	public abstract class Layout2D : Layout<ILayoutItem, RectTransform, Vector2>
	{

		public override void ArrangeItem(RectTransform target,
		                                 Vector2 position) => target.anchoredPosition = position;


		public override Vector2 GetInitialPosition() => _transforms[0].anchoredPosition;


		public override Vector2 GetNewPosition(RectTransform prev,
		                                       RectTransform current) => prev.anchoredPosition + GetPreviousOffset(prev: prev) + GetCurrentOffset(current: current);






	}

}
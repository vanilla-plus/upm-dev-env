using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Arrangement;
using Vanilla.Catalogue;
using Vanilla.Pools;

using static UnityEngine.Vector2;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class ArrangedLibrary2D<C, CI, LI, PO, A> : ArrangedLibrary<C, CI, LI, PO, RectTransform, A, Vector2>
		where C : Catalogue<CI>
		where CI : CatalogueItem
		where LI : ArrangedLibraryItem2D<LI, CI>
		where PO : Pool<LI>
		where A : Arrangement2D<LI>
	{

		public RectTransform focusParent;

		public Vector2 focusTargetPosition;

		private Vector2 focusVelocity;

		public float focusDuration = 0.1666f;

		public float focusDistanceThreshold = 0.01f;

		protected override void FocusFrame(LI item)
		{
			focusTargetPosition = SmoothDamp(current: focusTargetPosition,
			                                         target: item.Transform.anchoredPosition,
			                                         currentVelocity: ref focusVelocity,
			                                         smoothTime: focusDuration);

			focusParent.anchoredPosition = -focusTargetPosition;
		}


		protected override bool FocusWhile(LI item,
		                                   Toggle selectedToggle) => selectedToggle.State
		                                                          && Distance(a: focusTargetPosition,
		                                                                      b: item.Transform.anchoredPosition)
		                                                           > focusDistanceThreshold;

	}

}
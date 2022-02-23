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

		public Vector2 focusTargetDelta;

		private Vector2 focusVelocity;

		public bool  useDynamicSmoothStepTime = true;

		public float focusTimeMin             = 0.01f;
		public float focusTimeMax             = 0.1666f;
		
		public float focusTimeDistanceCheckScalar = 0.001f;
		public float focusWhileDistanceThreshold  = 0.01f;

		[Header("Debug")]
		public float distSqrMag;
		public float smoothStepTimeLerp;


		protected override void FocusFrame()
		{
			if (useDynamicSmoothStepTime)
			{
				var dest = _focusTargetTransform.anchoredPosition;

				distSqrMag = (dest - focusTargetDelta).sqrMagnitude;

				smoothStepTimeLerp = Mathf.Lerp(a: focusTimeMin,
				                                b: focusTimeMax,
				                                t: distSqrMag * focusTimeDistanceCheckScalar);

				focusTargetDelta = SmoothDamp(current: focusTargetDelta,
				                              target: dest,
				                              currentVelocity: ref focusVelocity,
				                              smoothTime: smoothStepTimeLerp);
			}
			else
			{
				focusTargetDelta = SmoothDamp(current: focusTargetDelta,
				                              target: _focusTargetTransform.anchoredPosition,
				                              currentVelocity: ref focusVelocity,
				                              smoothTime: focusTimeMax);
			}

			focusParent.anchoredPosition = -focusTargetDelta;
		}


//		protected override bool FocusWhile(LI item,
//		                                   Toggle selectedToggle) => selectedToggle.State
//		                                                          && Distance(a: focusTargetDelta,
//		                                                                      b: item.Transform.anchoredPosition)
//		                                                           > focusDistanceThreshold;


		protected override bool FocusWhile() => Distance(a: focusTargetDelta,
		                                                 b: _focusTargetTransform.anchoredPosition)
		                                      > focusWhileDistanceThreshold;

	}

}
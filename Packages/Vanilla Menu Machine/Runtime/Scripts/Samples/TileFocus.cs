using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.MediaLibrary
{

	public class TileFocus : MonoBehaviour
	{

		public RectTransform _rect;

		public RectTransform target;

		[Header(header: "Settings")]
		public float smoothTime = 0.1666f;
		public float smoothDistanceEpsilon = 0.001f;

		[Header(header: "State")]
		public Vector2 smoothPosition;
		public Vector2 smoothPositionTarget;
		public Vector2 smoothVelocity;

		public float smoothDistance;

		public bool lockedOn;


		public void ChangeTarget(RectTransform newTarget)
		{
			target   = newTarget;

			lockedOn = false;
		}


		void LateUpdate()
		{
			smoothPositionTarget = _rect.InverseTransformPoint(position: target.position);

			smoothDistance = Vector2.Distance(a: smoothPosition,
			                                  b: smoothPositionTarget);

			if (smoothDistance < smoothDistanceEpsilon) lockedOn = true;

			if (lockedOn)
			{
				_rect.anchoredPosition = -smoothPositionTarget;
			}
			else
			{
				smoothPosition = Vector2.SmoothDamp(current: smoothPosition,
				                                    target: smoothPositionTarget,
				                                    currentVelocity: ref smoothVelocity,
				                                    smoothTime: smoothTime);

				_rect.anchoredPosition = -smoothPosition;

			}

//			Canvas.ForceUpdateCanvases();
		}

	}

}
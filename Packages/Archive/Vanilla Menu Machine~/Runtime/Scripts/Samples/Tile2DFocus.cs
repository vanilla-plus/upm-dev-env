using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public class Tile2DFocus : TileFocus<Tile2D, RectTransform>
	{

		[Header(header: "References")]
		public RectTransform _rect;
		
		[Header(header: "Settings")]
		public float smoothTime = 0.1666f;
		public float smoothDistanceEpsilon = 0.001f;

		[Header(header: "State")]
		public RectTransform target;

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


		public override void OnItemAdded(Tile2D item) => item.selected.Active.onTrue += () => ChangeTarget(newTarget: item.Transform);

		public override void OnItemRemoved(Tile2D item) { }

		public override void Update() { }


		public override void LateUpdate()
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
		}

	}

}
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.MenuMachine
{

	[Serializable]
	public class Layout2DHorizontal : Layout2D<Layout2DHorizontal>
	{

		[SerializeField]
		public float leftMargin = 25.0f;

		[SerializeField]
		public float rightMargin = 25.0f;

		[Tooltip("Use this to modify the direction of the layout. -1 will flip the direction entirely.")]
		[SerializeField]
		public float offsetScalar = 1.0f;

		protected Layout2DHorizontal(LayoutItem2D[] items) : base(items) { }


		public override Vector2 GetPreviousOffset(RectTransform prev) => new(x: offsetScalar * (prev.sizeDelta.x * 0.5f + rightMargin),
		                                                                     y: 0.0f);


		public override Vector2 GetCurrentOffset(RectTransform current) => new(x: offsetScalar * (current.sizeDelta.x * 0.5f + leftMargin),
		                                                                       y: 0.0f);

	}

}
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public class Layout2DVertical : Layout2D<Layout2DVertical>
	{
		
		public float topMargin  = 25.0f;
		public float bottomMargin = 25.0f;

		[Tooltip("Use this to modify the direction of the layout. -1 will flip the direction entirely.")]
		[SerializeField]
		public float offsetScalar = 1.0f;

		protected Layout2DVertical(LayoutItem2D[] items) : base(items) { }


		public override Vector2 GetPreviousOffset(RectTransform prev) => new(x: 0.0f,
		                                                                     y: offsetScalar * (prev.sizeDelta.y * 0.5f + bottomMargin));


		public override Vector2 GetCurrentOffset(RectTransform current) => new(x: 0.0f,
		                                                                       y: offsetScalar * (current.sizeDelta.y * 0.5f + topMargin));

	}

}
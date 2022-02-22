//using System;
//
//using UnityEngine;
//
//namespace Vanilla.Arrangement
//{
//
//	[Serializable]
//	public class Arrangement2DVertical<I> : Arrangement2D<I>
//		where I : IArrangementItem
//	{
//
//		public float topMargin    = 25.0f;
//		public float bottomMargin = 25.0f;
//
//		[Tooltip("Use this to modify the direction of the layout. -1 will flip the direction entirely.")]
//		[SerializeField]
//		public float offsetScalar = 1.0f;
//
//		public override Vector2 GetPreviousOffset(RectTransform prev) => new(x: 0.0f,
//		                                                                     y: offsetScalar * (prev.sizeDelta.y * 0.5f + bottomMargin));
//
//
//		public override Vector2 GetCurrentOffset(RectTransform current) => new(x: 0.0f,
//		                                                                       y: offsetScalar * (current.sizeDelta.y * 0.5f + topMargin));
//
//	}
//
//}
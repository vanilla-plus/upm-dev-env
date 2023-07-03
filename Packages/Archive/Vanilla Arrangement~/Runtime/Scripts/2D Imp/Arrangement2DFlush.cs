using System;

using UnityEngine;

namespace Vanilla.Arrangement
{

	[Serializable]
	public class Arrangement2DFlush<I> : Arrangement2D<I>
		where I : class, IArrangementItem<RectTransform>
	{

		[SerializeField] public float topMargin = 25.0f;

		[SerializeField] public float bottomMargin = 25.0f;

		[SerializeField] public float leftMargin = 25.0f;

		[SerializeField] public float rightMargin = 25.0f;

		[Tooltip("Use this to modify the horizontal direction of the layout. -1 will flip the direction entirely.")] [SerializeField]
		public float horizontalOffsetScalar = 1.0f;

		[Tooltip("Use this to modify the vertical direction of the layout. -1 will flip the direction entirely.")] [SerializeField]
		public float verticalOffsetScalar = 1.0f;


		public override Vector2 GetPreviousOffset(I prev)
		{
			var size = prev.Transform.sizeDelta;

			return new Vector2(x: horizontalOffsetScalar * (size.x * 0.5f + rightMargin),
			                   y: verticalOffsetScalar   * (size.y * 0.5f + bottomMargin));
		}


		public override Vector2 GetCurrentOffset(I current)
		{
			var size = current.Transform.sizeDelta;

			return new Vector2(x: horizontalOffsetScalar * (size.x * 0.5f + leftMargin),
			                   y: verticalOffsetScalar   * (size.y * 0.5f + topMargin));
		}


		public override void ArrangeFrame()
		{
			base.ArrangeFrame();
			
			
		}

	}

}
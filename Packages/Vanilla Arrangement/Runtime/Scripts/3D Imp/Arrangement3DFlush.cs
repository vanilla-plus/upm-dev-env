using System;

using UnityEngine;

namespace Vanilla.Arrangement
{

	[Serializable]
	public class Arrangement3DFlush<I> : Arrangement3D<I>
		where I : IArrangementItem
	{

		[SerializeField] public float topMargin = 0.5f;

		[SerializeField] public float bottomMargin = 0.5f;

		[SerializeField] public float leftMargin = 0.5f;

		[SerializeField] public float rightMargin = 0.5f;

		[SerializeField] public float forwardMargin = 0.5f;

		[SerializeField] public float backMargin = 0.5f;

		[Tooltip("Use this to modify the horizontal direction of the layout. -1 will flip the direction entirely.")] [SerializeField]
		public float horizontalOffsetScalar = 1.0f;

		[Tooltip("Use this to modify the vertical direction of the layout. -1 will flip the direction entirely.")] [SerializeField]
		public float verticalOffsetScalar = 1.0f;

		[Tooltip("Use this to modify the forwards/backwards direction of the layout. -1 will flip the direction entirely.")] [SerializeField]
		public float depthOffsetScalar = 1.0f;


		public override Vector3 GetPreviousOffset(Transform prev)
		{
			var size = prev.localScale;

			return new Vector3(x: horizontalOffsetScalar * (size.x * 0.5f + rightMargin),
			                   y: verticalOffsetScalar   * (size.y * 0.5f + bottomMargin),
			                   z: depthOffsetScalar      * (size.z * 0.5f + backMargin));
		}


		public override Vector3 GetCurrentOffset(Transform current)
		{
			var size = current.localScale;

			return new Vector3(x: horizontalOffsetScalar * (size.x * 0.5f + leftMargin),
			                   y: verticalOffsetScalar   * (size.y * 0.5f + topMargin),
			                   z: depthOffsetScalar      * (size.z * 0.5f + forwardMargin));
		}

	}

}
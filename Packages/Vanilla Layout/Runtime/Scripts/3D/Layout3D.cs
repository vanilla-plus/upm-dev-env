using System;

using UnityEngine;

namespace Vanilla.Layout
{

	[Serializable]
	public abstract class Layout3D : LayoutBase<LayoutItem3D, Transform, Vector3>
	{

		public override void ArrangeItem(Transform target,
		                                 Vector3 position) => target.localPosition = position;


		public override Vector3 GetInitialPosition() => _transforms[0].localPosition;


		public override Vector3 GetNewPosition(Transform prev,
		                                       Transform current) => prev.localPosition + GetPreviousOffset(prev: prev) + GetCurrentOffset(current: current);






	}

}
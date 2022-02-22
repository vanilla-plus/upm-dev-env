using System;

using UnityEngine;

namespace Vanilla.Arrangement
{

	[Serializable]
	public abstract class Arrangement3D<I> : Arrangement<I, Transform, Vector3>
		where I : IArrangementItem
	{

		public override void ArrangeItem(Transform target,
		                                 Vector3 position) => target.localPosition = position;


		public override Vector3 GetInitialPosition() => _transforms[0].localPosition;


		public override Vector3 GetNewPosition(Transform prev,
		                                       Transform current) => prev.localPosition + GetPreviousOffset(prev: prev) + GetCurrentOffset(current: current);






	}

}
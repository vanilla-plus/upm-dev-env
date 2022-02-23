using System;

using UnityEngine;

namespace Vanilla.Arrangement
{

	[Serializable]
	public abstract class Arrangement3D<I> : Arrangement<I, Transform, Vector3>
		where I : class, IArrangementItem<Transform>
	{

		public override void ArrangeItem(I current,
		                                 Vector3 position) => current.Transform.localPosition = position;


		public override Vector3 GetInitialPosition(I current) => current.Transform.localPosition;


		public override Vector3 GetNewPosition(I prev,
		                                       I current) => prev.Transform.localPosition + GetPreviousOffset(prev: prev) + GetCurrentOffset(current: current);






	}

}
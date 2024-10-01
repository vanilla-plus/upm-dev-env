using UnityEngine;

using Vanilla.Easing;

using Vanilla.TypeMenu;

namespace MagicalProject
{

	public class EulerRotateEach : AnimationArray
	{

		[SerializeField]
		public Vector3 fromEulers = Vector3.zero;

		[SerializeField]
		public Vector3 toEulers = new Vector3(x: 180,
		                                      y: -180,
		                                      z: 180);
		
		[TypeMenu("blue")]
		[SerializeReference]
		public IEasingSlot easingSlot = new Power_In_Out();


		protected override void AnimateTransform(Transform transform,
		                                         float timeSlice) => transform.localEulerAngles = Vector3.Lerp(a: fromEulers,
		                                                                                                       b: toEulers,
		                                                                                                       t: easingSlot.Ease(timeSlice));

	}

}
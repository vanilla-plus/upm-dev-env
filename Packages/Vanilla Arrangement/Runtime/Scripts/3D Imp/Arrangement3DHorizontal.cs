using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.Arrangement
{

	[Serializable]
	public class Arrangement3DHorizontal : Arrangement3D
	{

		[SerializeField]
		public float leftMargin = 0.5f;

		[SerializeField]
		public float rightMargin = 0.5f;

		[Tooltip("Use this to modify the direction of the layout. -1 will flip the direction entirely.")]
		[SerializeField]
		public float offsetScalar = 1.0f;



		public override Vector3 GetPreviousOffset(Transform prev) => new(x: offsetScalar * (prev.localScale.x * 0.5f + rightMargin),
		                                                                 y: 0.0f,
		                                                                 z: 0.0f);


		public override Vector3 GetCurrentOffset(Transform current) => new(x: offsetScalar * (current.localScale.x * 0.5f + leftMargin),
		                                                                   y: 0.0f,
		                                                                   z: 0.0f);

	}

}
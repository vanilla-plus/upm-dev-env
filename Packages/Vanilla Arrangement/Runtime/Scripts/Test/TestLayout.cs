using System;

using UnityEngine;

namespace Vanilla.Arrangement
{

	[Serializable]
	public abstract class TestLayout<L, I, T, P> : MonoBehaviour
		where L : Arrangement<I, T, P>
		where I : IArrangementItem
		where T : Transform
		where P : struct
	{

		[SerializeField]
		public L layout;

		protected virtual void Awake() => layout.Populate(transform);

	}

}
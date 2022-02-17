using System;

using UnityEngine;

namespace Vanilla.Layout
{

	[Serializable]
	public abstract class TestLayout<L, I, T, P> : MonoBehaviour
		where L : Layout<I, T, P>
		where I : ILayoutItem
		where T : Transform
		where P : struct
	{

		[SerializeField]
		public L layout;

		protected virtual void Awake() => layout.Populate(transform);

	}

}
using System;

using UnityEngine;

namespace Vanilla.Layout
{

	[Serializable]
	public abstract class TestLayout<L, I, T, P> : MonoBehaviour
		where L : LayoutBase<I, T, P>
		where I : LayoutItem<T>
		where T : Transform
		where P : struct
	{

		[SerializeField]
		public L layout;

		protected virtual void Awake() => layout.Populate(transform);

	}

}
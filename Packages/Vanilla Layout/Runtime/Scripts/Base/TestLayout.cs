using System;

using UnityEngine;

namespace Vanilla.Layout
{

	[Serializable]
	public abstract class TestLayout<L, I, T, P> : MonoBehaviour
		where L : ILayout<I, T, P>
		where I : ILayoutItem<T>
		where T : Transform
		where P : struct
	{

		[SerializeField]
		public L layout;

		void Awake()
		{
			layout.Populate(transform);
			
			layout.OnArrangeComplete += () =>
			                            {
				                            foreach (var t in layout.Transforms)
				                            {
					                            t.hasChanged = false;
				                            }
			                            };
		}


		//		void Update() => layout.ArrangeItems();

	}

}
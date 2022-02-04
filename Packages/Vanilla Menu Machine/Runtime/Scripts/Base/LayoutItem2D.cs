using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.MenuMachine
{

	public abstract class LayoutItem2D : MonoBehaviour,
	                                     ILayoutItem<RectTransform>
	{

		[SerializeField]
		private RectTransform _transform;
		public RectTransform Transform
		{
			get => _transform;
			set => _transform = value;
		}
		
		[SerializeField]
		internal RectTransform _previous;
		public RectTransform Previous
		{
			get => _previous;
			set => _previous = value;
		}

		public virtual void Awake() => _transform = (RectTransform)transform;

	}

}
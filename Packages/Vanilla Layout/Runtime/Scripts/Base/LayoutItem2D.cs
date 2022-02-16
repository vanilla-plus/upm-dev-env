using System;

using UnityEngine;

namespace Vanilla.Layout
{

	[Serializable]
	public class LayoutItem2D : MonoBehaviour,
	                            ILayoutItem<RectTransform>
	{

		[SerializeField]
		protected bool _isDirty;
		public bool IsDirty => _isDirty;



		public RectTransform Transform() => (RectTransform) transform;

		void Update() => _isDirty = transform.hasChanged;

	}

}
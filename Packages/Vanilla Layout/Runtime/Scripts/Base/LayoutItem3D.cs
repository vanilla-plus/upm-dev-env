using System;

using UnityEngine;

namespace Vanilla.Layout
{

	[Serializable]
	public class LayoutItem3D : MonoBehaviour,
	                            ILayoutItem<Transform>
	{

		[SerializeField]
		protected bool _isDirty;
		public bool IsDirty => _isDirty;

		public Transform Transform() => transform;

	}

}
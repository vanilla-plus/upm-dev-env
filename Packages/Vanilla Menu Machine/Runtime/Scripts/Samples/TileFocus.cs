using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.MediaLibrary
{

	public abstract class TileFocus<I,T> : ITileGroupModule<I,T>
		where I : Tile<I,T>
		where T : Transform
	{

		private HashSet<I> _items;
		public  HashSet<I> Items
		{
			get => _items;
			set => _items = value;
		}

		public abstract void OnItemAdded(I item);

		public abstract void OnItemRemoved(I item);

		public abstract void Update();

		public abstract void LateUpdate();

	}

}
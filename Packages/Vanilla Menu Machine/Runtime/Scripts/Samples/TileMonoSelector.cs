using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class TileMonoSelector<I,T> : ITileGroupModule<I,T>
		where I : Tile<I,T>
		where T : Transform
	{

		private        HashSet<I> _items;
		public         HashSet<I> Items
		{
			get => _items;
			set => _items = value;
		}

		private static I          _current;
		public static  I          Current => _current;

		public void OnItemAdded(I item) { }

		public void OnItemRemoved(I item) { }

		public         void Update() { }

		public void LateUpdate() { }
		
		public void Track()
		{
			foreach (var t in _items) t.selected.Toggle.onTrue += () => HandleCandidateSelection(selected: t);
		}


		public void StopTracking()
		{
			foreach (var t in _items) t.selected.Toggle.onTrue -= () => HandleCandidateSelection(selected: t);
		}


		private void HandleCandidateSelection(I selected)
		{
			if (_current)
			{
				_current.selected.Toggle.State = false;
			}

			_current = selected;
		}




	}

}
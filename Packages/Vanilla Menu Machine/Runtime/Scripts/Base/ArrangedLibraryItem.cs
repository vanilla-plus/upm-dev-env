using System;

using UnityEngine;

using Vanilla.Arrangement;
using Vanilla.Catalogue;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class ArrangedLibraryItem<LI, CI, T> : LibraryItem<LI, CI, T>,
	                                                       IArrangementItem<T>
		where LI : ArrangedLibraryItem<LI, CI, T>
		where CI : ICatalogueItem
		where T : Transform
	{

		public bool arrangementIsDirtiedByPointerHover = true;
		public bool arrangementIsDirtiedByPointerDown;
		public bool arrangementIsDirtiedByPointerSelect = true;
		
		[SerializeField] private Toggle _arrangementDirty = new(startingState: false);
		public                   Toggle ArrangementDirty => _arrangementDirty;


		protected override void Awake()
		{
			base.Awake();

			if (arrangementIsDirtiedByPointerHover)
			{
				PointerHover.Normal.Empty.onFalse += () => ArrangementDirty.State = AssessIfDirty();
				PointerHover.Normal.Full.onFalse  += () => ArrangementDirty.State = AssessIfDirty();

				PointerHover.Normal.Full.onTrue  += () => ArrangementDirty.State = AssessIfDirty();
				PointerHover.Normal.Empty.onTrue += () => ArrangementDirty.State = AssessIfDirty();
			}

			if (arrangementIsDirtiedByPointerDown)
			{
				PointerDown.Normal.Empty.onFalse += () => ArrangementDirty.State = AssessIfDirty();
				PointerDown.Normal.Full.onFalse  += () => ArrangementDirty.State = AssessIfDirty();

				PointerDown.Normal.Full.onTrue  += () => ArrangementDirty.State = AssessIfDirty();
				PointerDown.Normal.Empty.onTrue += () => ArrangementDirty.State = AssessIfDirty();
			}

			if (arrangementIsDirtiedByPointerSelect)
			{
				PointerSelected.Normal.Empty.onFalse += () => ArrangementDirty.State = AssessIfDirty();
				PointerSelected.Normal.Full.onFalse  += () => ArrangementDirty.State = AssessIfDirty();

				PointerSelected.Normal.Full.onTrue  += () => ArrangementDirty.State = AssessIfDirty();
				PointerSelected.Normal.Empty.onTrue += () => ArrangementDirty.State = AssessIfDirty();
			}
		}


		protected virtual bool AssessIfDirty()
		{
//			return true;
			
			if (arrangementIsDirtiedByPointerHover)
			{
				if (!PointerHover.Normal.IsFullOrEmpty()) return true;
			}

			if (arrangementIsDirtiedByPointerDown)
			{
				if (!PointerDown.Normal.IsFullOrEmpty()) return true;
			}

			if (arrangementIsDirtiedByPointerSelect)
			{
				if (!PointerSelected.Normal.IsFullOrEmpty()) return true;
			}

			return false;
		}

	}

}
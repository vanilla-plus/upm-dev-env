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
				PointerHover.Normal.Empty.onFalse += () => ArrangementDirty.State = true;
				PointerHover.Normal.Full.onFalse  += () => ArrangementDirty.State = true;

				PointerHover.Normal.Full.onTrue  += () => ArrangementDirty.State = false;
				PointerHover.Normal.Empty.onTrue += () => ArrangementDirty.State = false;
			}

			if (arrangementIsDirtiedByPointerDown)
			{
				PointerDown.Normal.Empty.onFalse += () => ArrangementDirty.State = true;
				PointerDown.Normal.Full.onFalse  += () => ArrangementDirty.State = true;

				PointerDown.Normal.Full.onTrue  += () => ArrangementDirty.State = false;
				PointerDown.Normal.Empty.onTrue += () => ArrangementDirty.State = false;
			}
			
			if (arrangementIsDirtiedByPointerSelect)
			{
				PointerSelected.Normal.Empty.onFalse += () => ArrangementDirty.State = true;
				PointerSelected.Normal.Full.onFalse  += () => ArrangementDirty.State = true;

				PointerSelected.Normal.Full.onTrue  += () => ArrangementDirty.State = false;
				PointerSelected.Normal.Empty.onTrue += () => ArrangementDirty.State = false;
			}
		}

	}

}
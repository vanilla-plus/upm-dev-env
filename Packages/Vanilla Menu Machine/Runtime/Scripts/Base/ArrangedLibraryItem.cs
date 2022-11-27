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
		
		[SerializeField] private SmartBool _arrangementDirty = new(startingValue: false);
		public                   SmartBool ArrangementDirty => _arrangementDirty;


		protected override void Awake()
		{
			base.Awake();

			if (arrangementIsDirtiedByPointerHover)
			{
				PointerHover.Progress.AtMin.onFalse += () => ArrangementDirty.Value = AssessIfDirty();
				PointerHover.Progress.AtMax.onFalse += () => ArrangementDirty.Value = AssessIfDirty();

				PointerHover.Progress.AtMax.onTrue  += () => ArrangementDirty.Value = AssessIfDirty();
				PointerHover.Progress.AtMin.onTrue += () => ArrangementDirty.Value = AssessIfDirty();
			}

			if (arrangementIsDirtiedByPointerDown)
			{
				PointerDown.Progress.AtMin.onFalse += () => ArrangementDirty.Value = AssessIfDirty();
				PointerDown.Progress.AtMax.onFalse  += () => ArrangementDirty.Value = AssessIfDirty();

				PointerDown.Progress.AtMax.onTrue  += () => ArrangementDirty.Value = AssessIfDirty();
				PointerDown.Progress.AtMin.onTrue += () => ArrangementDirty.Value = AssessIfDirty();
			}

			if (arrangementIsDirtiedByPointerSelect)
			{
				PointerSelected.Progress.AtMin.onFalse += () => ArrangementDirty.Value = AssessIfDirty();
				PointerSelected.Progress.AtMax.onFalse  += () => ArrangementDirty.Value = AssessIfDirty();

				PointerSelected.Progress.AtMax.onTrue  += () => ArrangementDirty.Value = AssessIfDirty();
				PointerSelected.Progress.AtMin.onTrue += () => ArrangementDirty.Value = AssessIfDirty();
			}
		}


		protected virtual bool AssessIfDirty()
		{
//			return true;
			
			if (arrangementIsDirtiedByPointerHover)
			{
				if (!PointerHover.Progress.AtMinOrMax()) return true;
			}

			if (arrangementIsDirtiedByPointerDown)
			{
				if (!PointerDown.Progress.AtMinOrMax()) return true;
			}

			if (arrangementIsDirtiedByPointerSelect)
			{
				if (!PointerSelected.Progress.AtMinOrMax()) return true;
			}

			return false;
		}

	}

}
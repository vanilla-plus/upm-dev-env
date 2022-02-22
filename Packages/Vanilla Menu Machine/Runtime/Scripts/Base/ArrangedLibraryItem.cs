using System;

using UnityEngine;

using Vanilla.Arrangement;
using Vanilla.Catalogue;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class ArrangedLibraryItem<LI, CI, T> : LibraryItem<LI, CI, T>,
	                                                       IArrangementItem
		where LI : ArrangedLibraryItem<LI, CI, T>
		where CI : ICatalogueItem
		where T : Transform
	{

		[SerializeField] private Toggle _arrangementDirty = new(startingState: false);
		public                   Toggle ArrangementDirty => _arrangementDirty;


		protected override void Awake()
		{
			base.Awake();

			PointerSelected.Normal.Empty.onFalse += () => ArrangementDirty.State = true;
			PointerSelected.Normal.Full.onFalse  += () => ArrangementDirty.State = true;

			PointerSelected.Normal.Full.onTrue  += () => ArrangementDirty.State = false;
			PointerSelected.Normal.Empty.onTrue += () => ArrangementDirty.State = false;
		}

	}

}
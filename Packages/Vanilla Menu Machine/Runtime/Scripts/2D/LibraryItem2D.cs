using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Catalogue;
using Vanilla.Easing;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class LibraryItem2D<CI> : LibraryItem<CI, RectTransform>
		where CI : ICatalogueItem

	{

		protected override void Awake()
		{
			base.Awake();

			var originalSize = Transform.sizeDelta;

			PointerHover.normal.OnChange += n => Transform.sizeDelta = new Vector2(x: originalSize.x + originalSize.x * n.InOutPower(power: 1.25f),
			                                                                       y: originalSize.y);

			PointerHover.normal.Empty.onFalse += () => Dirty.State = true;
			PointerHover.normal.Full.onFalse  += () => Dirty.State = true;

			PointerHover.normal.Full.onTrue  += () => Dirty.State = false;
			PointerHover.normal.Empty.onTrue += () => Dirty.State = false;
		}

	}

}
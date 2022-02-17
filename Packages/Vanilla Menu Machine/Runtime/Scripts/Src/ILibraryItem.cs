using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.Catalogue;

namespace Vanilla.MediaLibrary
{

	public interface ILibraryItem<CI>
		where CI : ICatalogueItem
	{

		CI CatalogueItem
		{
			get;
		}

		State PointerHover
		{
			get;
		}

		State PointerDown
		{
			get;
		}

		State PointerSelected
		{
			get;
		}
		
		

	}

}
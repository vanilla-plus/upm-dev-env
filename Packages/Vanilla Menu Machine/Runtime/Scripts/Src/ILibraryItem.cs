using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Vanilla.Catalogue;
using Vanilla.PointerRedirect;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary
{

	public interface ILibraryItem<CI, T> : IPoolItem,
	                                       IPointerRedirectTarget
		where CI : ICatalogueItem
		where T : Transform
	{

		T Transform
		{
			get;
		}

		CI Payload
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
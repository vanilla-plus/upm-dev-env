using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.MediaLibrary
{

	public interface ILayout<L, I, T, P>
		where L : ILayout<L, I, T, P>
		where I : ILayoutItem<T>
		where T : Transform
		where P : struct
	{

		I[] Items
		{
			get;
		}

		void Arrange();

		P GetInitialPosition();

		P GetPreviousOffset(T prev);

		P GetCurrentOffset(T current);

	}

}
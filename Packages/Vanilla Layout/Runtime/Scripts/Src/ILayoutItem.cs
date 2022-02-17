using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.Layout
{

	public interface ILayoutItem<T>
		where T : Transform
	{

		T Transform
		{
			get;
		}

		Toggle Dirty
		{
			get;
		}

		Toggle Hover
		{
			get;
		}

		Toggle Selected
		{
			get;
		}


	}

}
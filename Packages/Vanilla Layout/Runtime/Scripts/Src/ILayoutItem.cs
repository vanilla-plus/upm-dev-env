using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.Layout
{

	public interface ILayoutItem<T>
		where T : Transform
	{

		bool IsDirty
		{
			get;
		}


//		T Transform
//		{
//			get;
//			set;
//		}

	}

}
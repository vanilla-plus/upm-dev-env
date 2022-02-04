using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.MenuMachine
{

	public interface ILayoutItem<T>
		where T : Transform
	{

		T Transform
		{
			get;
			set;
		}
		
		T Previous
		{
			get;
			set;
		}

	}

}
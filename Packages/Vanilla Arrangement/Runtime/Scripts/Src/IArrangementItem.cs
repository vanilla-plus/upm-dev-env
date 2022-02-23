using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.Arrangement
{

	public interface IArrangementItem<T>
		where T : Transform
	{

		T Transform
		{
			get;
		}

		Toggle ArrangementDirty
		{
			get;
		}

//		Toggle Hover
//		{
//			get;
//		}
//
//		Toggle Selected
//		{
//			get;
//		}


	}

}
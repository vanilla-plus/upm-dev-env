using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.Arrangement
{

	public interface IArrangement<I, T, P>
		where I : IArrangementItem
		where T : Transform
		where P : struct
	{

		I[] Items
		{
			get;
		}

		T[] Transforms
		{
			get;
		}

		bool ArrangementInProgress
		{
			get;
		}
		
		bool ArrangementRequired();

		void Populate(Transform parent);

		void InvokeArrangement();
		
		void ArrangeFrame();


		public void ArrangeItem(T target,
		                        P position);


		P GetInitialPosition() => default;


		P GetNewPosition(T prev,
		                 T current);


		P GetPreviousOffset(T prev);

		P GetCurrentOffset(T current);

		Action OnArrangeBegun
		{
			get;
			set;
		}

		Action OnArrangeComplete
		{
			get;
			set;
		}

	}

}
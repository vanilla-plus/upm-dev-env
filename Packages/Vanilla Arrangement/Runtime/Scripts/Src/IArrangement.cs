using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.Arrangement
{

	public interface IArrangement<I, T, P>
		where I : class, IArrangementItem<T>
		where T : Transform
		where P : struct
	{

		T Parent
		{
			get;
		}
		
		HashSet<I> Items
		{
			get;
		}

		public bool ForceArrangement
		{
			get;
			set;
		}
		
		SmartBool ArrangementInProgress
		{
			get;
		}
		
		bool ArrangementRequired();

		void Populate();

//		void InvokeArrangement();
		
		void ArrangeFrame();


		public void ArrangeItem(I current,
		                        P position);


		P GetInitialPosition(I current) => default;


		P GetNewPosition(I prev,
		                 I current);


		P GetPreviousOffset(I prev);

		P GetCurrentOffset(I current);

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
using System;

using UnityEngine;

namespace Vanilla.MetaScript.Drivers.Float
{

	[Serializable]
	public abstract class Normal_Base<T> : Module
	{

		public    T From;
		public    T To;

		protected abstract T Interpolate(float normal);
		
		public override void Init(Driver driver) => TryConnectSet(driver);


		public override void DeInit(Driver driver) => TryDisconnectSet(driver);

		protected override void HandleSet(float incoming) => Debug.Log(Interpolate(incoming));

		
		
	}

}
using System;

using UnityEngine;

using Vanilla.Init;
using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers
{

	[Serializable]
	public abstract class DriverInstance<T,S,A,M,D> : MonoBehaviour,
	                                                  IInitiable
		where S : IDataSource<T>
		where A : DataAsset<T,S>
		where M : Module<T,S,A,M,D>
		where D : Driver<T,S,A,M,D>
	{

		public enum WakeMethod
		{

			Awake,
			Start,
			Init,
			PostInit,
			OnEnable

		}

		[SerializeField]
		public WakeMethod wakeMethod = WakeMethod.Init;

		public abstract D[] Drivers
		{
			get;
		}


		private void OnValidate()
		{
			#if UNITY_EDITOR
			foreach (var driver in Drivers) driver?.OnValidate();
			#endif
		}


		void Awake()
		{
			if (wakeMethod != WakeMethod.Awake) return;

			foreach (var driver in Drivers) driver?.Init();
		}


		void Start()
		{
			if (wakeMethod != WakeMethod.Start) return;

			foreach (var driver in Drivers) driver?.Init();
		}


		public void Init()
		{
			if (wakeMethod != WakeMethod.Init) return;

			foreach (var driver in Drivers) driver?.Init();
		}


		public void PostInit()
		{
			if (wakeMethod != WakeMethod.PostInit) return;

			foreach (var driver in Drivers) driver?.Init();
		}


		void OnEnable()
		{
			if (wakeMethod != WakeMethod.OnEnable) return;

			foreach (var driver in Drivers) driver?.Init();
		}


		void OnDisable()
		{
			if (wakeMethod != WakeMethod.OnEnable) return;

			foreach (var driver in Drivers) driver?.DeInit();
		}


		private void OnDestroy()
		{
			if (wakeMethod == WakeMethod.OnEnable) return;

			foreach (var driver in Drivers) driver?.DeInit();
		}

	}

}
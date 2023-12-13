using System;

using UnityEngine;

using Vanilla.Init;
using Vanilla.TypeMenu;

namespace Vanilla.Drivers
{

	[Serializable]
	public abstract class DriverInstance<T> : MonoBehaviour,
	                                          IInitiable
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

		public abstract DriverSocket<T>[] Sockets
		{
			get;
		}


		private void OnValidate()
		{
			#if UNITY_EDITOR
			foreach (var set in Sockets) set?.OnValidate();
			#endif
		}


		void Awake()
		{
			if (wakeMethod != WakeMethod.Awake) return;

			foreach (var set in Sockets) set?.Init();
		}


		void Start()
		{
			if (wakeMethod != WakeMethod.Start) return;

			foreach (var set in Sockets) set?.Init();
		}


		public void Init()
		{
			if (wakeMethod != WakeMethod.Init) return;

			foreach (var set in Sockets) set?.Init();
		}


		public void PostInit()
		{
			if (wakeMethod != WakeMethod.PostInit) return;

			foreach (var set in Sockets) set?.Init();
		}


		void OnEnable()
		{
			if (wakeMethod != WakeMethod.OnEnable) return;

			foreach (var set in Sockets) set?.Init();
		}


		void OnDisable()
		{
			if (wakeMethod != WakeMethod.OnEnable) return;

			foreach (var set in Sockets) set?.DeInit();
		}


		private void OnDestroy()
		{
			if (wakeMethod == WakeMethod.OnEnable) return;

			foreach (var set in Sockets) set?.DeInit();
		}

	}

}
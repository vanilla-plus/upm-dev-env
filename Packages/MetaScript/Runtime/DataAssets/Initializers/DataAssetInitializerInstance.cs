using System;

using UnityEngine;

using Vanilla.Init;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets.Initializers
{

	[Serializable]
	public class DataAssetInitializerInstance : MonoBehaviour,
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

		[TypeMenu("red")]
		[SerializeReference]
		public IDataInitializer[] Initializers = Array.Empty<IDataInitializer>();

		private void OnValidate()
		{
			#if UNITY_EDITOR
			foreach (var i in Initializers) i?.Initialize();
			#endif
		}


		void Awake()
		{
			if (wakeMethod != WakeMethod.Awake) return;

			foreach (var i in Initializers) i?.Initialize();
		}


		void Start()
		{
			if (wakeMethod != WakeMethod.Start) return;

			foreach (var i in Initializers) i?.Initialize();
		}


		public void Init()
		{
			if (wakeMethod != WakeMethod.Init) return;

			foreach (var i in Initializers) i?.Initialize();
		}


		public void PostInit()
		{
			if (wakeMethod != WakeMethod.PostInit) return;

			foreach (var i in Initializers) i?.Initialize();
		}


		void OnEnable()
		{
			if (wakeMethod != WakeMethod.OnEnable) return;

			foreach (var i in Initializers) i?.Initialize();
		}


		void OnDisable()
		{
			if (wakeMethod != WakeMethod.OnEnable) return;

			foreach (var i in Initializers) i?.Initialize();
		}


		private void OnDestroy()
		{
			if (wakeMethod == WakeMethod.OnEnable) return;

			foreach (var i in Initializers) i?.Initialize();
		}

	}

}
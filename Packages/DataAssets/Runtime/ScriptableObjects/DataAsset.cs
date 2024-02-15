using System;

using UnityEngine;

using Vanilla.DataSources;

namespace Vanilla.DataAssets
{

	[Serializable]
	public abstract class DataAsset<T> : ScriptableObject
	{

		public abstract IDataSource<T> Source
		{
			get;
			set;
		}
		
		public void Awake() => Debug.Log("Awake");
		
		public void OnDestroy() => Debug.Log("OnDestroy");
		
		public void OnEnable()
		{
			Debug.Log("OnEnable");

//			if (!Application.isPlaying) return;
			
			Source?.Init();
		}


		public void OnDisable()
		{
			Debug.Log("OnDisable");

//			if (!Application.isPlaying) return;

			Source?.Deinit();
		}

	}

}
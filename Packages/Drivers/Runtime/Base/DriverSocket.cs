using System;

using UnityEngine;

using Vanilla.DataAssets;

namespace Vanilla.Drivers
{

	[Serializable]
	public abstract class DriverSocket<T>
	{

//		[SerializeField]
		public abstract DataAsset<T> Asset
		{
			get;
		}
        
		public abstract Module<T>[] Modules
		{
			get;
		}
        
		public virtual void OnValidate()
		{
			#if UNITY_EDITOR
			foreach (var d in Modules) d?.OnValidate(this);
			#endif
		}


		public virtual void Init()
		{
			foreach (var d in Modules) d?.Init(this);
		}


		public virtual void DeInit()
		{
			foreach (var d in Modules) d?.DeInit(this);
		}

	}

}
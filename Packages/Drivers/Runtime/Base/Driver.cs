using System;

using UnityEngine;

using Vanilla.DataAssets;

namespace Vanilla.Drivers
{

	[Serializable]
	public abstract class Driver<T>
	{

		[SerializeField]
		public T InitialValue;
		
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
			if (Asset == null) return;

			Asset.Source.Value = InitialValue;

			foreach (var module in Modules) module?.OnValidate(this);
			#endif
		}


		public virtual void Init()
		{
			if (Asset == null)
			{
				Debug.LogError($"[{GetType().Name}] is missing it's asset.");
				
				return;
			}
			
			if (Asset.Source == null)
			{
				Debug.LogError($"[{Asset.name}] has a null Source.");
				
				return;
			}
			
			Asset.Source.Value = InitialValue;

			foreach (var module in Modules) module?.Init(this);
		}


		public virtual void DeInit()
		{
			foreach (var d in Modules) d?.DeInit(this);
		}

	}

}
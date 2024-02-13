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

			foreach (var d in Modules) d?.OnValidate(this);
			#endif
		}


		public virtual void Init()
		{
			if (Asset == null)
			{
				Debug.LogError($"[{GetType().Name}] is missing it's asset.");
				
				return;
			}
			
			Asset.Source.Value = InitialValue;

			foreach (var d in Modules) d?.Init(this);

			//			Asset.Delta.Value = InitialValue;

			// I think the idea was that this set would invoke HandleValueChange
			// But thanks to set-defense, it might not always fire.
			// I think in the name of safety, set before initting (like above)
			// And then manually call a HandleValueChange in each modules Init.
		}


		public virtual void DeInit()
		{
			foreach (var d in Modules) d?.DeInit(this);
		}

	}

}
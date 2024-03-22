using System;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers
{

	[Serializable]
	public abstract class Driver<T,S,A,M,D>
		where S : IDataSource<T>
		where A : DataAsset<T,S>
		where M : Module<T,S,A,M,D>
		where D : Driver<T,S,A,M,D>
	{

		[SerializeField]
		[HideInInspector]
		private string Name;
		
		[SerializeField]
		public T InitialValue;
		
		public abstract A Asset
		{
			get;
		}
        
		public abstract M[] Modules
		{
			get;
		}
        
		public virtual void OnValidate()
		{
			#if UNITY_EDITOR
			if (Asset        == null ||
			    Asset.Source == null)
			{
				Name = "No Asset/Source assigned";
				return;
			}

			Name = $"{Asset.name} [{Asset.Source.GetType().Name}]";

//			Debug.LogWarning(Asset.Source.Value);
			
			Asset.Source.Value = InitialValue;

//			Debug.LogError(Asset.Source.Value);

			foreach (var module in Modules) module?.OnValidate(this as D);
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

			foreach (var module in Modules) module?.Init(this as D);
		}


		public virtual void DeInit()
		{
			foreach (var d in Modules) d?.DeInit(this as D);
		}

	}

}
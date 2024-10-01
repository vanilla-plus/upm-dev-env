using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{

	// This was created so that collections of assets could be possible and iterated over together.
	// Anything that pertains to Assets in a payload-agnostic way should go in here.

	[Serializable]
	public abstract class DataAsset<T, S> : BaseAsset
		where S : class, IGettableSource<T>, ISettableSource<T>, IObservableSource<T>
	{

		public T DefaultValue;

		public abstract S Source
		{
			get;
			set;
		}


		public override void Reset()
		{
			base.Reset();

			if (Source == null)
			{
				Debug.LogError($"[{name}] can't reset because it has a null DataSource.");

				return;
			}

			Source.Value = DefaultValue;
		}


		protected override void OnEnable()
		{
			base.OnEnable();

			if (Source == null)
			{
				Debug.LogError($"[{Time.frameCount}] {name} has a null DataSource!");

				return;
			}

			#if debug
			Debug.Log($"[{Time.frameCount}] {name}\t=> OnEnable");
			#endif

			Source.Value = DefaultValue;
		}

	}

}
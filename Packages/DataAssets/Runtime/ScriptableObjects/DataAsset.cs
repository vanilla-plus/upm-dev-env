using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets
{

	[Serializable]
	public abstract class DataAsset<T> : ScriptableObject
	{
		
		public abstract DeltaValue<T> Delta
		{
			get;
			set;
		}

	}

}
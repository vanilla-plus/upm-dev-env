using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	public abstract class DataAsset<T> : ScriptableObject
	{

		public abstract IDataSource<T> Source
		{
			get;
			set;
		}

	}

}
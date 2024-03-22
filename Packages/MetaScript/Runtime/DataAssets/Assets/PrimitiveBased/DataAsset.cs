using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	public abstract class DataAsset<T,S> : ScriptableObject
		where S : IDataSource<T>
	{

		public abstract S Source
		{
			get;
			set;
		}

	}

}
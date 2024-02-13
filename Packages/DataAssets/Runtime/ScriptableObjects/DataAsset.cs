using System;

using UnityEngine;

using Vanilla.DataSources;

namespace Vanilla.DataAssets
{

	[Serializable]
	public abstract class DataAsset<T> : ScriptableObject
	{

		//        string Name
//        {
//            get;
//            set;
//        }
		
		public abstract IDataSource<T> Source
		{
			get;
			set;
		}

//		public abstract DeltaValue<T> Delta
//		{
//			get;
//			set;
//		}

	}

}
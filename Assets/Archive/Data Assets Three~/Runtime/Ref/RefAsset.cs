using System;

using UnityEngine;

namespace Vanilla.DataAssets.Three
{

	[Serializable]
	public abstract class RefAsset<TType, TSource> : GenericAsset<TType, TSource>
		where TType : class
		where TSource : RefSource<TType>
	{


	}

}
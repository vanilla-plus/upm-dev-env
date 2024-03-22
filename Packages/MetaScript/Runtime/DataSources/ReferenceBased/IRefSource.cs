using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
	public interface IRefSource<T,S> : IDataSource<T>
		where T : class
		where S : IRefSource<T,S>
	{
		
	}
}
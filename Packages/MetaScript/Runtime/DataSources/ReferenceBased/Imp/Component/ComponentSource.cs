using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources.GenericComponent
{
	
	public interface IComponentSource<T,S> : IRefSource<T,S>
		where T : Component
		where S : IComponentSource<T,S>
	{
		
	}
}
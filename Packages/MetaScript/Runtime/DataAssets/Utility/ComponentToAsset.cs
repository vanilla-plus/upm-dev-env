using System;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources.GenericComponent;

namespace Vanilla.MetaScript
{

	[Serializable]
	public abstract class ComponentToAsset<T,S> : MonoBehaviour
		where T : Component
		where S : class, IComponentSource<T,S>
	{

		[SerializeField]
		public ComponentAsset<T,S> Asset;

		void OnEnable()
		{
			if (Asset        != null &&
			    Asset.Source != null)
			{
				Asset.Assign(gameObject);
//				Asset.Source.Value = GetComponent<T>();
			}
		}


		void OnDisable()
		{
			if (ReferenceEquals(objA: Asset.Source.Value,
			                    objB: gameObject)) Asset.Source.Value = null;
		}

	}

}
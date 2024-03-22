using UnityEngine;

using Vanilla.MetaScript.DataSources.GenericComponent;

namespace Vanilla.MetaScript.DataAssets
{
	
	public abstract class ComponentAssetBinder<T,S,A> : MonoBehaviour 
		where T : Component
		where S : class, IComponentSource<T,S>
		where A : ComponentAsset<T,S>
	{

		[SerializeField]
		public A Asset;
        
		public void OnEnable() => Asset.Source.Value = GetComponent<T>();

		public void OnDisable() => Asset.Source.Value = null;
		
	}

}
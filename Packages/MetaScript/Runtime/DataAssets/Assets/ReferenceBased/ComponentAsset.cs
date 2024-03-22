using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources.GenericComponent;

namespace Vanilla.MetaScript.DataAssets
{

//	[Serializable]
//	[CreateAssetMenu(fileName = "GameObject Asset",
//	                 menuName = "Vanilla/Data Assets/GameObject",
//	                 order = 8)]
	public abstract class ComponentAsset<T,S> : RefAsset<T,S> 
		where T : Component
		where S : class, IComponentSource<T,S>
	{

//		[SerializeReference]
//		[TypeMenu("yellow")]
//		private S _source;
//		public override IDataSource<T> Source
//		{
//			get => _source;
//			set => _source = value as S;
//		}

	}

}
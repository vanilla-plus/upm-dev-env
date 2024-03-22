using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources.GameObject;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{

	[Serializable]
	[CreateAssetMenu(fileName = "GameObject Asset",
	                 menuName = "Vanilla/Data Assets/GameObject",
	                 order = 8)]
	public class GameObjectAsset : RefAsset<GameObject, IGameObjectSource>
	{
		
		[SerializeReference]
		[TypeMenu("yellow")]
		private IGameObjectSource _source;

		public override IGameObjectSource Source
		{
			get => _source;
			set => _source = value;
		}

	}

}
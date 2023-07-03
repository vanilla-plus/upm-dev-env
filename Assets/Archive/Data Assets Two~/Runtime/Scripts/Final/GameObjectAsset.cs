using System;

using UnityEngine;

namespace Vanilla.DataAssets // ------------------------------------------------------------------------------------------------------ GameObject //
{

	[Serializable]
	public class GameObjectSocket : RefSocket<GameObject,
		GameObjectSocket,
		GameObjectAsset,
		GameObjectAccessor> // ----------------------------------------------------------------------------------------------------------- Socket //
	{ }

	[CreateAssetMenu(fileName = "New GameObject Asset",
	                 menuName = "Vanilla/Data Assets/Ref/GameObject")]
	[Serializable]
	public class GameObjectAsset : RefAsset<GameObject,
		GameObjectSocket,
		GameObjectAsset,
		GameObjectAccessor> // ------------------------------------------------------------------------------------------------------------ Asset //
	{ }

	[Serializable]
	public abstract class GameObjectAccessor : RefAccessor<GameObject,
		GameObjectSocket,
		GameObjectAsset,
		GameObjectAccessor> // -------------------------------------------------------------------------------------------------------- Processors //
	{ }

}
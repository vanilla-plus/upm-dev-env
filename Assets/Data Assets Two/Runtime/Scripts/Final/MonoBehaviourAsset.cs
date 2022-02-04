using System;

using UnityEngine;

namespace Vanilla.DataAssets // --------------------------------------------------------------------------------------------------- MonoBehaviour //
{

	[Serializable]
	public class MonoBehaviourSocket : RefSocket<MonoBehaviour,
		MonoBehaviourSocket,
		MonoBehaviourAsset,
		MonoBehaviourAccessor> // -------------------------------------------------------------------------------------------------------- Socket //
	{ }

	[CreateAssetMenu(fileName = "New MonoBehaviour Asset",
	                 menuName = "Vanilla/Data Assets/Ref/MonoBehaviour")]
	[Serializable]
	public class MonoBehaviourAsset : RefAsset<MonoBehaviour,
		MonoBehaviourSocket,
		MonoBehaviourAsset,
		MonoBehaviourAccessor> // --------------------------------------------------------------------------------------------------------- Asset //
	{ }

	[Serializable]
	public abstract class MonoBehaviourAccessor : RefAccessor<MonoBehaviour,
		MonoBehaviourSocket,
		MonoBehaviourAsset,
		MonoBehaviourAccessor> // ----------------------------------------------------------------------------------------------------- Processors //
	{ }

}
using System;

using UnityEngine;

namespace Vanilla.DataAssets // ------------------------------------------------------------------------------------------------------- Transform //
{

	[Serializable]
	public class TransformSocket : RefSocket<Transform,
		TransformSocket,
		TransformAsset,
		TransformAccessor> // ------------------------------------------------------------------------------------------------------------ Socket //
	{ }

	[CreateAssetMenu(fileName = "New Transform Asset",
	                 menuName = "Vanilla/Data Assets/Ref/Transform")]
	[Serializable]
	public class TransformAsset : RefAsset<Transform,
		TransformSocket,
		TransformAsset,
		TransformAccessor> // ------------------------------------------------------------------------------------------------------------- Asset //
	{ }

	[Serializable]
	public abstract class TransformAccessor : RefAccessor<Transform,
		TransformSocket,
		TransformAsset,
		TransformAccessor> // --------------------------------------------------------------------------------------------------------- Processors //
	{ }

}
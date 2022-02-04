using System;

using UnityEngine;

namespace Vanilla.DataAssets // ------------------------------------------------------------------------------------------------------ Quaternion //
{

	[Serializable]
	public class QuaternionSocket : StructSocket<Quaternion, QuaternionSocket, QuaternionAsset, QuaternionAccessor> // ------------------- Socket //
	{ }

	[CreateAssetMenu(fileName = "New Quaternion Data Asset",
	                 menuName = "Vanilla/Data Assets/Struct/Quaternion")]
	[Serializable]
	public class QuaternionAsset : StructAsset<Quaternion, QuaternionSocket, QuaternionAsset, QuaternionAccessor> // ---------------------- Asset //
	{ }

	[Serializable]
	public abstract class QuaternionAccessor : StructAccessor<Quaternion, QuaternionSocket, QuaternionAsset, QuaternionAccessor> // --- Processors //
	{ }

}
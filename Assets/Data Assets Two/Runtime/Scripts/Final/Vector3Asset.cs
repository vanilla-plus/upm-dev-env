using System;

using UnityEngine;

namespace Vanilla.DataAssets // --------------------------------------------------------------------------------------------------------- Vector3 //
{

	[Serializable]
	public class Vector3Socket : StructSocket<Vector3, Vector3Socket, Vector3Asset, Vector3Accessor> // ---------------------------------- Socket //
	{ }

	[CreateAssetMenu(fileName = "New Vector3 Data Asset",
	                 menuName = "Vanilla/Data Assets/Struct/Vector3")]
	[Serializable]
	public class Vector3Asset : StructAsset<Vector3, Vector3Socket, Vector3Asset, Vector3Accessor> // ------------------------------------- Asset //
	{ }

	[Serializable]
	public abstract class Vector3Accessor : StructAccessor<Vector3, Vector3Socket, Vector3Asset, Vector3Accessor> // ------------------ Processors //
	{ }

}
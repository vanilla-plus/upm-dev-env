using System;

using UnityEngine;

namespace Vanilla.DataAssets // --------------------------------------------------------------------------------------------------------- Vector2 //
{

	[Serializable]
	public class Vector2Socket : StructSocket<Vector2, Vector2Socket, Vector2Asset, Vector2Accessor> // ---------------------------------- Socket //
	{ }

	[CreateAssetMenu(fileName = "New Vector2 Data Asset",
	                 menuName = "Vanilla/Data Assets/Struct/Vector2")]
	[Serializable]
	public class Vector2Asset : StructAsset<Vector2, Vector2Socket, Vector2Asset, Vector2Accessor> // ------------------------------------- Asset //
	{ }

	[Serializable]
	public abstract class Vector2Accessor : StructAccessor<Vector2, Vector2Socket, Vector2Asset, Vector2Accessor> // ------------------ Processors //
	{ }

}
using System;

using UnityEngine;

namespace Vanilla.DataAssets.Three
{

	[CreateAssetMenu(fileName = "[Vector3] ",
	                 menuName = "Vanilla/Data Assets/3/Struct/Vector3")]
	[Serializable]
	public class Vector3Asset : StructAsset<Vector3, Vector3Source> { }

}
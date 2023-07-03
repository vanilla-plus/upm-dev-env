using System;

using UnityEngine;

namespace Vanilla.DataAssets.Three
{

	[CreateAssetMenu(fileName = "[Float] ",
	                 menuName = "Vanilla/Data Assets/3/Value/Float")]
	[Serializable]
	public class FloatAsset : ValueAsset<float, FloatSource> { }

}
using System;

using UnityEngine;

namespace Vanilla.DataAssets.Three
{

	[CreateAssetMenu(fileName = "[Int] ",
	                 menuName = "Vanilla/Data Assets/3/Value/Int")]
	[Serializable]
	public class IntAsset : ValueAsset<int, IntSource> { }

}
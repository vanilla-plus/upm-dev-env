using System;

using UnityEngine;

using static UnityEngine.Debug;

namespace Vanilla.DataAssets.Three
{

	[CreateAssetMenu(fileName = "[Bool] ",
	                 menuName = "Vanilla/Data Assets/3/Value/Bool")]
	[Serializable]
	public class BoolAsset : ValueAsset<bool, BoolSource>
	{

		[ContextMenu("Log Get() to console")]
		public void LogGet() => Log(source.Get().ToString());

	}

}
using System;

using UnityEngine;

namespace Vanilla.DataAssets
{

	// ----------------------------------------------------------------------------------------------------------------------------------- String //

	// ----------------------------------------------------------------------------------------------------------------------------------- Socket //

	[Serializable]
	public class StringSocket : RefSocket<string, StringSocket, StringAsset, StringAccessor> { }

	// ------------------------------------------------------------------------------------------------------------------------------------ Asset //

	[CreateAssetMenu(fileName = "New String Asset",
	                 menuName = "Vanilla/Data Assets/Ref/String")]
	[Serializable]
	public class StringAsset : RefAsset<string, StringSocket, StringAsset, StringAccessor> { }

	// ------------------------------------------------------------------------------------------------------------------------------- Processors //

	[Serializable]
	public abstract class StringAccessor : RefAccessor<string, StringSocket, StringAsset, StringAccessor> { }

}
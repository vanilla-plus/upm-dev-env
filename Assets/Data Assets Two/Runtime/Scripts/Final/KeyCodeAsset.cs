using System;

using UnityEngine;

namespace Vanilla.DataAssets // --------------------------------------------------------------------------------------------------------- KeyCode //
{

	[Serializable]
	public class KeyCodeSocket : ValueSocket<KeyCode, KeyCodeSocket, KeyCodeAsset, KeyCodeAccessor> // ----------------------------------- Socket //
	{ }

	[CreateAssetMenu(fileName = "New KeyCode Data Asset",
	                 menuName = "Vanilla/Data Assets/Value/KeyCode")]
	[Serializable]
	public class KeyCodeAsset : ValueAsset<KeyCode, KeyCodeSocket, KeyCodeAsset, KeyCodeAccessor> // -------------------------------------- Asset //
	{ }

	[Serializable]
	public abstract class KeyCodeAccessor : ValueAccessor<KeyCode, KeyCodeSocket, KeyCodeAsset, KeyCodeAccessor> // ------------------- Processors //
	{ }

}
using UnityEditor;

using Vanilla.ContextMenuTools.Editor;

namespace Vanilla.DebugCam.Editor
{

	public class DebugCamHierarchyMenu : HierarchyMenuBase
	{

		private const string DebugCamKeyword = "Debug Cam";


		[MenuItem(itemName: GameObjectSubPath + VanillaSubPath + DebugCamKeyword,
		          isValidateFunction: false,
		          priority: Priority)]
		public static void InstantiateDebugCam(MenuCommand menuCommand) =>
			CreatePrefabInstance(menuCommand: menuCommand,
			                     packageName: "debug-cam",
			                     prefabName: "Debug Cam");

	}

}
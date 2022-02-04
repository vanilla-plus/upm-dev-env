#if UNITY_EDITOR

using System.Linq;

using UnityEditor;

using UnityEngine;

namespace Vanilla.ContextMenuTools.Editor
{
	public abstract class HierarchyMenuBase : ContextMenuBase
	{
		
		protected const int Priority = 1;


		public static void CreatePrefabInstance(MenuCommand menuCommand,
		                                        string      packageName = "meshkit",
		                                        string      subPath     = RuntimeSubPath + PrefabsSubPath,
		                                        string      prefabName  = "Sphere [High]")
		{
			var path = $"{PackagesSubPath}{VanillaPackageKeyword}{packageName}/{subPath}{prefabName}{PrefabExtensionKeyword}";

			Debug.Log(path);
			
//			var search = AssetDatabase.FindAssets(prefabName);

//			Debug.Log(AssetDatabase.GUIDToAssetPath(search[0]));
			
			var asset = (GameObject) AssetDatabase.LoadAssetAtPath(assetPath: path,
			                                                       type: typeof(GameObject));

			if (asset == null)
			{
				Debug.LogWarning(message: $"We were unable to find the chosen prefab. It was expected to be found at the following path:\n\n{path}");

				return;
			}

			var instance = PrefabUtility.InstantiatePrefab(assetComponentOrGameObject: asset) as GameObject;

			if (instance == null)
			{
				Debug.LogWarning(message: $"The prefab [{prefabName}] could not be instantiated. Is the prefab file corrupted?");

				return;
			}

			GameObjectUtility.SetParentAndAlign(child: instance,
			                                    parent: menuCommand.context as GameObject);

			Undo.RegisterCreatedObjectUndo(objectToUndo: instance,
			                               name: "Create instance of " + instance.name);

			Selection.activeGameObject = instance;
		}

	}
}

#endif